# InstrumentHub

InstrumentHub, müzik aletleri satışı için geliştirilmiş, modern ve ölçeklenebilir bir e-ticaret platformudur. Proje, .NET 8 ve Entity Framework Core kullanılarak katmanlı mimariyle tasarlanmıştır. Kullanıcılar ürünleri inceleyebilir, filtreleyebilir, sepetlerine ekleyebilir ve güvenli ödeme işlemleri gerçekleştirebilirler.

## İçindekiler

- [Genel Bakış](#genel-bakış)
- [Teknolojiler](#teknolojiler)
- [Katmanlı Mimari](#katmanlı-mimari)
- [Özellikler](#özellikler)
- [Kurulum ve Çalıştırma](#kurulum-ve-çalıştırma)
- [Veritabanı ve Migration](#veritabanı-ve-migration)
- [Katkıda Bulunanlar](#katkıda-bulunanlar)
- [Lisans](#lisans)

---

## Genel Bakış

InstrumentHub, müzik aletleri ve aksesuarlarının satışını kolaylaştırmak için geliştirilmiş bir platformdur. Kullanıcı dostu arayüzü ve güçlü altyapısı sayesinde hem müşteriler hem de yöneticiler için verimli bir alışveriş ve yönetim deneyimi sunar.

## Teknolojiler

- **Backend:** ASP.NET Core 8, Entity Framework Core 8, Microsoft Identity
- **Frontend:** Razor Pages, Bootstrap, jQuery
- **Veritabanı:** SQL Server
- **Ödeme:** Iyzipay entegrasyonu
- **Diğer:** Katmanlı mimari, Dependency Injection, Migration yönetimi

## Katmanlı Mimari

Proje aşağıdaki ana katmanlardan oluşur:

- **Entity Layer ([InstrumentHub.Entitys](InstrumentHub.Entitys/))**  
  Tüm temel veri modelleri (ör. `EProduct`, `Division`, `Cart`, `Order`) burada tanımlanır.

- **Data Access Layer ([InstrumentHub.DataAccess](InstrumentHub.DataAccess/))**  
  Veritabanı işlemleri, repository pattern ve migration yönetimi bu katmanda gerçekleştirilir.

- **Business Layer ([Instrument.Business](Instrument.Business/))**  
  İş kuralları, servisler ve iş mantığı burada bulunur.

- **Web UI ([InstrumentHub.WebUI](InstrumentHub.WebUI/))**  
  Kullanıcı arayüzü, controller'lar, view'lar ve kimlik doğrulama işlemleri bu katmanda yer alır.

## Özellikler

- **Kullanıcı Kayıt ve Giriş:**  
  E-posta doğrulamalı, güvenli kullanıcı yönetimi.

- **Ürün Yönetimi:**  
  Yöneticiler için ürün ekleme, düzenleme, silme ve kategori yönetimi.

- **Kategori (Division) Sistemi:**  
  Ürünler kategorilere (ör. Telli Çalgılar, Üflemeli Çalgılar) ayrılır.

- **Sepet ve Sipariş Yönetimi:**  
  Kullanıcılar ürünleri sepete ekleyip sipariş oluşturabilir.

- **Yorum ve Puanlama:**  
  Kullanıcılar ürünlere yorum yapabilir ve puan verebilir.

- **Güvenli Ödeme:**  
  Iyzipay ile entegre güvenli ödeme altyapısı.

- **Responsive Tasarım:**  
  Mobil ve masaüstü uyumlu modern arayüz.

## Kurulum ve Çalıştırma

1. **Projeyi Klonlayın:**
   ```sh
   git clone https://github.com/kullaniciadi/INSTRUMENTHUB.git
   cd INSTRUMENTHUB
   ```

2. **Veritabanı Ayarlarını Yapın:**
   - `InstrumentHub.WebUI/appsettings.json` ve `InstrumentHub.WebUI/appsettings.Development.json` dosyalarındaki bağlantı stringlerini kendi SQL Server bilgilerinizle güncelleyin.

3. **Migration ve Veritabanı Oluşturma:**
   - Paket Yöneticisi Konsolu'nda:
     ```sh
     cd InstrumentHub.WebUI
     dotnet ef database update
     ```

4. **Projeyi Çalıştırın:**
   - Visual Studio veya terminalden:
     ```sh
     dotnet run --project InstrumentHub.WebUI
     ```
   - Uygulama varsayılan olarak `https://localhost:7136` veya `https://localhost:7076` adresinde çalışacaktır.

## Veritabanı ve Migration

- Migration dosyaları [InstrumentHub.DataAccess/Migrations](InstrumentHub.DataAccess/Migrations/) klasöründe tutulur.
- Gerekirse yeni migration eklemek için:
  ```sh
  dotnet ef migrations add MigrationAdi --project InstrumentHub.DataAccess --startup-project InstrumentHub.WebUI
  dotnet ef database update --project InstrumentHub.DataAccess --startup-project InstrumentHub.WebUI
  ```

## Katkıda Bulunanlar

- [Sizin Adınız](https://github.com/sizin-github-adresiniz)
- Katkıda bulunmak için pull request gönderebilir veya issue açabilirsiniz.

## Lisans

Bu proje MIT lisansı ile lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakınız.

---

InstrumentHub ile ilgili sorularınız için lütfen iletişime geçin veya bir issue oluşturun!