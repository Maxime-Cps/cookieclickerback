using System.ComponentModel.DataAnnotations;

namespace CookieClicker.Dtos;

public record CookiesRequest(
    [Range(0, double.MaxValue)] double Cookies,
    [Range(0, int.MaxValue)] int NbClics = 0);
