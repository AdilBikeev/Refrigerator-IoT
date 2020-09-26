# Refrigerator-IoT

# Требования для запуска сервера
1. Установить утилиту `dotnet`

# Как запустить сервер ?
1. Заходим через консоль в папку с проектом `Refrigerator.csproj`
2. Пишем в консоли `dotnet run`

# Как запустить сервер с Docker ?
1. `docker build -t aspnetapp .`
2. `docker run -d -p 80:80 --name refrigirator aspnetapp`

# Как проверить работу сервера ?
Для теста используем Postman или ему подобные приложения
Доступные API методы
- `api/refrigerator` - возвращает список мест в холодильнике

# Поднятие БД локально
1. В файле appsettings.json для поля `RefrigeratorConnection` задаем строку подключения в БД:
    * Server=`{название сервера}`;
    * Initial Catalog=`{навзание БД}`;
    * User ID=`{Логин пользователя}`;
    * Password=`{Пароль пользователя}`;
2. Устанавливаем утилиту для EntityFramework с помощью команду `dotnet tool install --global dotnet-ef`
3. Строим файл, для миграции БД в СУБД с помощью команды `dotnet ef migrations add InitialMigration`
    (Для удаления не нужной миграции пишем `dotnet ef migrations remove`)
4. Обновляем БД по построенной схеме миграции `dotnet ef database update`