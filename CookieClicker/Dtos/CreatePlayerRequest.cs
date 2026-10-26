using System.ComponentModel.DataAnnotations;

namespace CookieClicker.Dtos;

public record CreatePlayerRequest([Required] string Name);