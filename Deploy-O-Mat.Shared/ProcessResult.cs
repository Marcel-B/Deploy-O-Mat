namespace com.b_velop.Deploy_O_Mat.Shared
{
    public class ProcessResult
    {
        public bool Success { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
        public int ReturnCode { get; set; }
    }
}