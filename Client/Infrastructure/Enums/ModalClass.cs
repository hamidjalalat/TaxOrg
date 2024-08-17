namespace Infrastructure.Enums
{
    public class ModalClass
    {
        public enum ModalSizeType : int
        {
            xl = 1,
            lg = 2,
            sm = 3,
        }
        
        public enum ModalIconType : int
        {
            Info = 1,
            Success = 2,
            Warning = 3,
            Error = 4,
        }
        
        public enum ModalStateType : int
        {
            Confirm = 1,
            Understood = 2,
            Close = 3,
        }
    }
}
