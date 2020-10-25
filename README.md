# Refrigerator-IoT

# Требования для запуска сервера
1. Установить утилиту `dotnet`
2. Установить MSSQL
3. Вставить свои настройки в `private readonly string` поля для взаимодействия с Azure в классе `AzureIOTAgent`

# Как запустить симулятор устройства
1. Заходим через консоль в папку с проектом `Refrigerator-IoT\SimulatedDevice\SimulatedDevice.csproj`
2. Пишем в консоли `dotnet run`
## Примечание
    * Для настройки симулятора устройства используйте приватные поля класса Startup: countRefri, countRefriBLock, countsensorData
    * Для добавления или изменения рандомных значений используйте классы, лежайшие в дириктории Mock
    * Установка частоты отправки сообщений на Azure IoT производится с помощью поля `perSeconds` класса `AzureIOTAgent`

# Как запустить CronJob для отправки данных с Azure IoT на сервер для обновления данных в БД
1. Заходим через консоль в папку с проектом `Refrigerator-IoT\CronJobAzureIoT\CronJobAzureIoT.csproj`
2. Пишем в консоли `dotnet run`

# Как запустить сервер ?
1. Заходим через консоль в папку с проектом `RefrigeratorServerSide.csproj`
2. Пишем в консоли `dotnet run`

# Как запустить сервер с Docker ?
1. `docker build -t aspnetapp .`
2. `docker run -d -p 80:80 --name refrigirator aspnetapp`

# Как проверить работу сервера ?
Для теста используем Postman или ему подобные приложения
Доступные API методы
- `api/refrigerator` - возвращает список мест в холодильнике

# Как запустить Swagger ?
1. Запускаете любым способом сервер
2. Переходите по ссылке: `https://localhost:{port}/swagger/index.html`  где `port` - порт, на котором запущен сервер

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

# Использование различных конфигураций
В решении доступны 2 вида конфигурации: RefrigeratorDebug, RefrigeratorRelease
Для запуска сервиса под нужную конфигурацию нужно выполнить следующую команду в консоли: `dotnet run --launch-profile Refrigerator{ Debug | Realese }`
    * Debug - использует набор статических данных
    * Release - использует Production версию сервиса