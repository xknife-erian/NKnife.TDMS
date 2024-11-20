using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NKnife.TDMS.Common
{
    /// <summary>
    /// 面向Channel里存储的数据集合的数据结构；用于将数据集合转换为指针和长度，以便传递给C++的DLL。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values"></param>
    internal class Data<T>(T[] values) : IDisposable where T : struct
    {
        private readonly T[] _values = values ?? throw new ArgumentNullException(nameof(values));
        private SafeBufferHandle _valuesHandle;
        private UIntPtr _lengthPtr;

        public (IntPtr Values, UIntPtr Length) GetValues()
        {
            if (_valuesHandle is { IsInvalid: false })
            {
                // 如果已经分配了内存，直接返回
                return (_valuesHandle.DangerousGetHandle(), _lengthPtr);
            }

            _lengthPtr = (UIntPtr)_values.Length;
            var size = Marshal.SizeOf<T>();
            _valuesHandle = new SafeBufferHandle(size * _values.Length);

            try
            {
                for (var i = 0; i < _values.Length; i++)
                {
                    Marshal.StructureToPtr(_values[i], _valuesHandle.DangerousGetHandle() + i * size, false);
                }
            }
            catch
            {
                // 如果发生异常，释放已分配的内存
                Dispose();
                throw;
            }

            return (_valuesHandle.DangerousGetHandle(), _lengthPtr);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _valuesHandle?.Dispose();
            }
        }

        ~Data()
        {
            Dispose(false);
        }
    }

    internal class SafeBufferHandle : SafeHandle
    {
        public SafeBufferHandle(int size) : base(IntPtr.Zero, true)
        {
            SetHandle(Marshal.AllocHGlobal(size));
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                Marshal.FreeHGlobal(handle);
                handle = IntPtr.Zero;
            }
            return true;
        }
    }
}
