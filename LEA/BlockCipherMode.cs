namespace LEA;

/// <summary>
/// 블록암호 운용모드 구현을 위한 인터페이스
/// </summary>
public abstract class BlockCipherMode
{
    public abstract PaddingBase? Padding { protected get; set; }

    /// <summary>
    /// 현재 객체가 구현하고 있는 알고리즘 이름을 리턴
    /// </summary>
    /// <returns>알고리즘 이름을 "블록암호/운용모드" 형태로 리턴</returns>
    public abstract string GetAlgorithmName();

    /// <summary>
    /// 입력에 대한 출력 길이를 계산
    /// </summary>
    /// <param name="length">
    ///            입력 길이</param>
    /// <returns>len 만큼의 입력을 처리하기 위해 필요한 출력 길이</returns>
    public abstract int GetOutputSize(int length);

    /// <summary>
    /// 부분 업데이트를 위해 필요한 출력의 길이 계산, 주로 블록크기의 배수로 계산됨
    /// </summary>
    /// <param name="length">
    ///            입력 길이</param>
    /// <returns>len 만큼의 입력을 처리하기 위해 필요한 중간 출력 길이</returns>
    public abstract int GetUpdateOutputSize(int length);

    /// <summary>
    /// IV를 필요로 하지 않는 운용모드를 위한 초기화 함수
    /// </summary>
    /// <param name="mode">
    ///            {@link BlockCipher.Mode}</param>
    /// <param name="key">
    ///            암호화 키</param>
    public abstract void Init(Mode mode, ReadOnlySpan<byte> key);

    /// <summary>
    /// IV를 필요로 하는 운용모드를 위한 초기화 함수
    /// </summary>
    /// <param name="mode">
    ///            {@link BlockCipher.Mode}</param>
    /// <param name="key">
    ///            암호화 키</param>
    /// <param name="iv">
    ///            초기화 벡터</param>
    public abstract void Init(Mode mode, ReadOnlySpan<byte> key, ReadOnlySpan<byte> iv);

    /// <summary>
    /// init을 완료한 상태로 변경, 새 메시지를 처리하기 위함
    /// </summary>
    public abstract void Reset();

    /// <summary>
    /// 온라인 모드를 위한 업데이트 함수
    /// </summary>
    /// <param name="message">
    ///            처리할 메시지 일부</param>
    /// <returns>처리된 메시지 일부</returns>
    public abstract ReadOnlySpan<byte> Update(ReadOnlySpan<byte> message);

    /// <summary>
    /// 암/복호화 최종단계 수행
    /// </summary>
    /// <param name="message">
    ///            평문/암호문의 마지막 부분</param>
    /// <returns>암호문 혹은 평문의 마지막 부분</returns>
    public abstract ReadOnlySpan<byte> DoFinal(ReadOnlySpan<byte> message);

    /// <summary>
    /// 암/복호화 최종단계 수행
    /// </summary>
    /// <returns>암호문 혹은 평문의 마지막 부분</returns>
    public abstract ReadOnlySpan<byte> DoFinal();
}