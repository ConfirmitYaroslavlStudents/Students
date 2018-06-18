CANDIDATE ACCOUNTING README


*** MongoDB ***

MongoDB: https://www.mongodb.com/download-center#community
Параметры установки по умолчанию.

1. Создайте базу данных с названием "CandidateAccounting" без пароля (можно создать базу с другим названием, но, в таком случае, необходимо обновить connectionURL для MongoDB в ./server/server.config.json (строка "databaseConnectionURL"))
2. Создайте три пустые коллекции:
    a. "accounts"
    b. "candidates"
    c. "tags"
3. ConnectionURL для MongoDB указан в ./server/server.config.json строка "databaseConnectionURL"
4. Подключение к базе данных осуществляется автоматически при запуске сервера


*** Scripts ***

    npm run build - запускает сборку клиента и сервера в папку dist
    npm run prod - выполняет build-скрипт и запускает сервер в prod-моде из папки dist
    npm run dev - запускает сервер в dev-моде


*** Configuration ***

Конфигурационный файл сервера (./server/server.config.json):
    port: номер порта, на котором работает сервер
    databaseConnectionURL: connectionURL для MongoDB

Конфигурационный файл авторизации (./server/authorization.config.json):
    allowedLogins: список логинов (email'ов) пользователей, для которых разрешены регистрация и вход (если пуст, то регистрация и вход разрешены для всех)
    sessionSecret: сессионный секретный ключ


*** Contacts ***

E-mail: dmitry.banokin@gmail.com