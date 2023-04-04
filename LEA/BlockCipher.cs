using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kr.Re.Nsr.Crypto
{
    /// <summary>
    /// 블록암호 한블록 암/복호화 구현을 위한 인터페이스
    /// </summary>
    public abstract class BlockCipher
    {
        public enum Mode
        {
            /// <summary>
            /// 암호화 모드
            /// </summary>
            ENCRYPT,
            /// <summary>
            /// 복호화 모드
            /// </summary>
            DECRYPT
        }

        /// <summary>
        /// 초기화 함수
        /// </summary>
        /// <param name="mode">
        ///            {@link BlockCipher.Mode}</param>
        /// <param name="mk">
        ///            암호화 키</param>
        public abstract void Init(Mode mode, byte[] mk);
        /// <summary>
        /// 새로운 데이터를 처리하기 위해 init을 수행한 상태로 복원
        /// </summary>
        public abstract void Reset();
        /// <summary>
        /// 암호화 알고리즘 이름을 리턴
        /// </summary>
        /// <returns>알고리즘 이름</returns>
        public abstract string GetAlgorithmName();
        /// <summary>
        /// 암호화 알고리즘의 한 블록 크기를 리턴
        /// </summary>
        /// <returns>한 블록 크기</returns>
        public abstract int GetBlockSize();
        /// <summary>
        /// 한블록 암호화
        /// </summary>
        /// <param name="in">
        ///            입력</param>
        /// <param name="inOff">
        ///            입력 시작 위치</param>
        /// <param name="out">
        ///            출력</param>
        /// <param name="outOff">
        ///            출력 시작 위치</param>
        /// <returns>처리한 데이터의 길이</returns>
        public abstract int ProcessBlock(byte[] @in, int inOff, byte[] @out, int outOff);
    }
}