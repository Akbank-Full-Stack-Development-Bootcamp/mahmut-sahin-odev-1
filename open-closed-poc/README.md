## SOLID'in O'su ##
![](https://miro.medium.com/max/1800/1*6qS_ikXaLI6VV1B666spiw.jpeg)
Nesne yönelimli programlama ile uğraşıp, SOLID Prensipleri’ni duymamış olma olasılığınız, benim NBA’de basketbol oynama olasılığım kadar düşüktür diyebiliriz sanırım :) SOLID Prensipleri genel olarak yazdığınız uygulamanın,
1. Daha anlaşılır,
2. Daha esnek,
3. Daha bakımı yapılabilir hale getirmeyi hedeflemektedir.

“Daha”ları belki arttırabiliriz, fakat SOLID prensiplerinin yazdığımız uygulamamız için pozitif olduğu aşikâr. SOLID Prensipleri 5 adet alt-prensipten oluşur ve her bir prensip belirli bir “eksiğe” odaklanır. Burada eksiği tırnak içine almamın sebebi şu ki, bu prensiplere uymayan programlar da gayet çalışabilir, yapması gereken görevi yapabilir. Fakat “What if” sorularını sormaya başladığınız zaman, eksiklikleri siz de fark edeceksinizdir.
***
Bu prensiplerinin neler olduğunu internetten çok kısa bir arama ile bulabilirsiniz. Bugün sizlere anlatmak istediğim, bu 5 arkadaştan sadece bir tanesi olan, “Open-Closed Principle”. Önce kısaca bu prensibin ne yaptığını açıklayacağım. Ardından da kısa bir örnek vereceğim.

O zaman önce dünyanın en anlaşılmayan tanımlarından biri olan, bu prensibin tanımı ile başlayalım: “Uygulama varlıkları (sınıflar, modüller, fonksiyonlar vs.) geliştirmeye AÇIK olmalı, fakat değişikliğe KAPALI olmalıdır”. Daha anlaşılabilir bir şekilde anlatmaya çalışırsam, bir uygulamaya, servise bir ekleme, geliştirme yapmak istediğiniz zaman, onun kaynak kodunu, servisini değiştirmek zorunda kalmadan bunu yapmanız gerekmektedir. Sözel olarak anlaması zor bir konu olduğundan, direkt örneğe geçiyorum.
***
Örneğimizde, bir banka için Web API uygulaması yazdığımızı düşüneceğiz. Kampanyaları olan bu banka, kampanya listesinde seçilmiş belirli özelliklerini gösterecek. Bir başka deyişle, “Kampanyalar Liste”sinde, kampanyaların özet bilgileri olacak ve bu bilgilerin ne olacağına biz karar vereceğiz. Uzatmadan örneğe geçiyorum.

Öncelikle iki adet modelimiz var. Bunlar,

1. Campaign => Kampanya ile alakalı bütün bilgilerin olduğu
```csharp
public class Campaign
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Desc { get; set; }
    public string? Conditions { get; set; }
    public bool IsActive { get; set; }
    public string ImageUrl { get; set; }
    public int CountLimit { get; set; }
}
```

2. CampaignsListResponseDTO => Kampanyalar sayfasına göndereceğimiz
```csharp
public class CampaignsListResponseDTO
{
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ImageUrl { get; set; }
}
```

Modellerimizi hazır olduğuna göre, şimdi asıl işi yapacak servisimi yazabiliriz. Fakat, önce prensibe uygun olmayan şekilde yazılmış kod ile başlayacağız. Hemen servisimizin ne yaptığına bakalım.

```csharp
public static List<CampaignsListResponseDTO> GetCampaignsListDTO()
{
    List<Campaign> campaignsRepo = CampaignRepository.InitRepo();
    List<CampaignsListResponseDTO> dtoList = new();

    campaignsRepo.ForEach(c =>
    {
        var campaignListObj = new CampaignsListResponseDTO
        {
            Name = c.Name,
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            ImageUrl = c.ImageUrl,
        };
        dtoList.Add(campaignListObj);
    });
    return dtoList;
}
```

Şimdi diyelim ki, ben artık kampanyalar sayfasında, o kampanyaların şartlarını da görmek istiyorum. Ne yapmam lazım o zaman? Adım adım gidelim:

1. DTO modelime o özelliği eklemem lazım
```csharp
public class CampaignsListResponseDTO
{
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string ImageUrl { get; set; }
    public string? Conditions { get; set; } // Ekledik.
}
```
2.Servisimizi de bu değişikliğe uygun bir şekilde değiştirmemiz lazım. Uygulamamızın kalbine girip, orada değişiklik yapmamız lazım yani?
```csharp
public static List<CampaignsListResponseDTO> GetCampaignsListDTO()
{
    List<Campaign> campaignsRepo = CampaignRepository.InitRepo();
    List<CampaignsListResponseDTO> dtoList = new();

    campaignsRepo.ForEach(c =>
    {
        var campaignListObj = new CampaignsListResponseDTO
        {
            Name = c.Name,
            StartTime = c.StartTime,
            EndTime = c.EndTime,
            ImageUrl = c.ImageUrl,
            Conditions = c.Conditions // Ekledik.
        };
        dtoList.Add(campaignListObj);
    });
    return dtoList;
}
```
Her şey tamam! Uygulamamız çatır çatır çalışıyor, artık kampanyaların şartlarını da gerekli yerde gösteriyor. FAKAT, ne demiştik, alt üstü çıktıya bir veri daha eklemek için uygulamamızın kalbinde bir değişiklik yaptık. Open-Closed tanımına göre konuşursak, bir geliştirme yapmak için değişiklik yapmak zorunda kaldık.

Peki iyi kod yazarak, prensibe uyarak, nasıl yazabilirdik? Gelin bir düşünelim. Biz şu an ne kadar DTO modelinde gönderilecek özellikleri seçsek de, serviste de bu değişikliği yapmak zorunda kalıyoruz. O zaman, servisteki işimizi otomatik hale getirirsek ne olur? Sadece DTO modelinde yaptığımız değişiklik ile gönderdiğimiz özellikleri değiştirir miyiz? Evet değiştiririz! Haydi yapalım bunu!
```csharp
public static List<CampaignsListResponseDTO> GetCampaignsListDTO()
{
    List<Campaign> campaignsRepo = CampaignRepository.InitRepo();
    List<CampaignsListResponseDTO> dtoList = new();
    Campaign campaign = new();
    CampaignsListResponseDTO campaignsListResponseDTO = new();
    var sourceProps = campaign.GetType().GetProperties();
    var dtoProps = campaignsListResponseDTO.GetType().GetProperties();
    foreach (var obj in campaignsRepo)
    {
        CampaignsListResponseDTO dtoCampaign = new();
        foreach (var sourceProp in sourceProps)
        {
            foreach (var dtoProp in dtoProps)
            {
                if (sourceProp.Name == dtoProp.Name && sourceProp.PropertyType == dtoProp.PropertyType)
                {
                    dtoProp.SetValue(dtoCampaign, sourceProp.GetValue(obj));
                }
            }
        }
        dtoList.Add(dtoCampaign);
    }
    return dtoList;
}
```
Yukarıda ne yaptık kısaca? Modellerimizin özelliklerini karşılaştırdık. Eğer isimleri ve veri tipleri aynı ise “if” koşuluna girerek, kampanya değerini, DTO özelliğine atadık. Bütün özellikleri bu şekilde atadıktan sonra, oluşan nesnemizi “return” edilecek DTO listemize ekledik.

Sonuç olarak, kodumuzu bu şekilde yazarak, servisimize hiç dokunmadan, gönderilecek özelliklere karar verdik ve prensibimize uygun bir kod yazdık.
