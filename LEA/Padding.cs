namespace LEA
{
	/// <summary>
	/// 패딩 구현을 위한 인터페이스
	/// </summary>
	public abstract class Padding
	{
		protected int blocksize;
		public Padding(int blocksize) => this.blocksize = blocksize;

		/// <summary>
		/// 패딩 추가
		/// </summary>
		/// <param name="in">
		///            패딩을 추가할 메시지, 길이가 블록사이즈보다 같거나 작아야 함</param>
		public abstract byte[] Pad(byte[] @in);
		/// <summary>
		/// 패딩 추가
		/// </summary>
		/// <param name="in">
		///            패딩을 추가할 메시지가 포함된 배열, 배열 전체의 길이는 블록암호 블록사이즈와 같아야 함</param>
		/// <param name="inOff">
		///            메시지 길이</param>
		public abstract void Pad(byte[] @in, int inOff);
		/// <summary>
		/// 패딩 제거
		/// </summary>
		/// <param name="in">
		///            패딩을 제거할 메시지</param>
		/// <returns>패딩이 제거된 메시지</returns>
		public abstract byte[] Unpad(byte[] @in);
		/// <summary>
		/// 패딩 길이 계산
		/// </summary>
		/// <param name="in">
		///            패딩이 포함된 메시지</param>
		/// <returns>패딩의 길이</returns>
		public abstract int GetPadCount(byte[] @in);
	}
}