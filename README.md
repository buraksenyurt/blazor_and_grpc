# Blazor ve gRPC

Bu örnekteki amacım Blazor uygulamalarında, gRPC servis kullanımını deneyimlemek. gRPC servisleri özellikle service-to-service iletişimde gecikme sürelerinin düşük, verimliliğin yüksek olması istenen hallerde sıklıkla tercih edilmekte. Protobuf protokolüne göre serileşen mesajların küçük boyutlu olması da cabası. gRPC ile ilgili belki de tek sıkıntı tarayıcı desteğinin olmaması. Karşı tarafta bir proto şemasının da bulunması gerekiyor. Bu noktada blazor ile iyi bir ikili olduklarını düşünebiliriz. Örnekte basit bir blazor uygulaması geliştirmeye çalışıp servis tabanlı iletişim için gRPC kullanmayı düşünüyorum.

## Setup

Örneğin Ubuntu 22.04 sisteminde, SQL için docker imajı kullanarak geliştirmekteyim. Editor olarak Visual Studio Code kullanıyorum.

## Ön Hazırlıklar

Bu sefer gigabyte'larıma kıydım ve SQL Server 2022 docker imajı kurmaya karar verdim.

```bash
# SQL docker örneğini başlatmak için (Şifreyi siz istediğiniz gibi verebilir veya aynısını kullanabilirsiniz)
sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=tig@r76!" -p 1433:1433 --name sql1 --hostname sql1 -d mcr.microsoft.com/mssql/server:2022-latest

# Container'ın çalıştığından emin olmak için
sudo docker ps -a

# terminalden docker içindeki sql komut satırına bağlanmak için
sudo docker exec -it sql1 "bash"
# ardından
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "tig@r76!"

# sistem veri tabanlarını görebiliyorsak işimiz tamamdır
SELECT Name FROM sys.Databases
GO
```

![assets/sql_cmd_01.png](assets/sql_cmd_01.png)

## EF Migration Tarafı

Entity Framework migration işlemleri için Microsoft.EntityFrameworkCore.Design paketinin projeye eklenmesi gerekiyor. Sonrasında örneğin Initial isimli migration planı aşağıdaki gibi oluşturulabilir.

```bash
dotnet ef migrations add Initial

# Aşağıdaki komutla da migration işletilebilir
dotnet ef database update
```

Eğer _database update_ işlemi başarılı bir şekilde çalıştıysa en azından komut satırından bile olsa veritabanımızı görmemiz lazım.

![assets/sql_cmd_02.png](assets/sql_cmd_02.png)

Pek tabi modelde değişiklikler yaparsak tekrardan bir migration planı oluşturup işletmemiz gerekiyor.