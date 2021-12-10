namespace RazorEX.BAL.DTOs.PostCommentsDTO
{
    public class CreatePostCommentDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
