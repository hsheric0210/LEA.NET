using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto
{
    /// <summary>
    /// MAC 구현을 위한 인터페이스
    /// </summary>
    public abstract class Mac
    {
        /// <summary>
        /// 초기화 함수
        /// </summary>
        /// <param name="key">
        ///            비밀키</param>
        public abstract void Init(byte[] key);
        /// <summary>
        /// 새로운 메시지에 대한 MAC 계산을 위한 객체 초기화
        /// </summary>
        public abstract void Reset();
        /// <summary>
        /// 메시지 추가
        /// </summary>
        /// <param name="msg">
        ///            추가할 메시지</param>
        public abstract void Update(byte[] msg);
        /// <summary>
        /// 마지막 메시지를 포함하여 MAC 계산
        /// </summary>
        /// <param name="msg">
        ///            마지막 메시지</param>
        /// <returns>MAC 값</returns>
        public abstract byte[] DoFinal(byte[] msg);
        /// <summary>
        /// 현재까지 추가된 메시지에 대한 MAC 계산
        /// </summary>
        /// <returns>MAC 값</returns>
        public abstract byte[] DoFinal();
    }
}