namespace mangaEnjoy.dto;

public class UserResponseDto
{
    public string Username { get; set; }

    public List<string> AddedMangas { get; set; } = [];
}