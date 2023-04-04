namespace LEA
{
	/// <summary>
	/// MAC 구현을 위한 인터페이스
	/// </summary>
	public abstract class MacBase
	{
		/// <summary>
		/// 초기화 함수
		/// </summary>
		/// <param name="key">
		///            비밀키</param>
		public abstract void Init(ReadOnlySpan<byte> key);

		/// <summary>
		/// 새로운 메시지에 대한 MAC 계산을 위한 객체 초기화
		/// </summary>
		public abstract void Reset();

		/// <summary>
		/// 메시지 추가
		/// </summary>
		/// <param name="message">
		///            추가할 메시지</param>
		public abstract void Update(ReadOnlySpan<byte> message);

		/// <summary>
		/// 마지막 메시지를 포함하여 MAC 계산
		/// </summary>
		/// <param name="message">
		///            마지막 메시지</param>
		/// <returns>MAC 값</returns>
		public abstract ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message);

		/// <summary>
		/// 현재까지 추가된 메시지에 대한 MAC 계산
		/// </summary>
		/// <returns>MAC 값</returns>
		public abstract ReadOnlySpan<byte> DoFinal();
	}
}