using GlassApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

public class SiparisController : Controller
{
    [HttpPost]
    public IActionResult Gonder(SiparisFormViewModel model)
    {
        // E-Posta ayarları
        var fromAddress = new MailAddress("furkanyilmaz870@gmail.com", "Web Sitesi");
        var toAddress = new MailAddress("ikindivaktici@hotmail.com");
        const string subject = "Yeni Ürün Siparişi";
        string body = $@"
            <h3>Yeni Sipariş</h3>
            <p><b>Ürün:</b> {model.UrunAdi}</p>
            <p><b>Ad Soyad:</b> {model.AdSoyad}</p>
            <p><b>Telefon:</b> {model.Telefon}</p>
            <p><b>Telefon:</b> {model.EPosta}</p>
            <p><b>Adres:</b> {model.Adres}</p>";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com", // Örneğin Gmail
            Port = 587,
            EnableSsl = true,
            Credentials = new NetworkCredential("furkanyilmaz870@gmail.com", "vzck yrld ttox yykt")
        };

        using var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        smtp.Send(message);

        TempData["Mesaj"] = "Siparişiniz başarıyla gönderildi.";
        return RedirectToAction("SiparisTesekkur");
    }

    public IActionResult SiparisTesekkur()
    {
        return View();
    }
}
