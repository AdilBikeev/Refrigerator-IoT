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
