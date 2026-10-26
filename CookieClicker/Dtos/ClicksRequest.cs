using System.ComponentModel.DataAnnotations;

namespace CookieClicker.Dtos;

public record ClicksRequest([Range(1, int.MaxValue)] int NbClics);