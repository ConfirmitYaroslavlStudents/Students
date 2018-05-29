CANDIDATE ACCOUNTING README


*** MongoDB ***

Работает и с Community версией: https://www.mongodb.com/download-center#community
Параметры установки по умолчанию.

1. Приложение ожидает сервер на порте 27017 (настраивается в server/mongoose.js connectionURL).
2. Необходимо создать базу данных с названием "CandidateAccounting" без пароля (настраивается в server/mongoose.js connectionURL)
3. В базе данных должно быть три коллекции:
    a. "accounts"
    b. "candidates"
    c. "tags"
4. Подключение к базе данных осуществляется автоматически при запуске сервера


*** Scripts ***

    npm run build-client-dev - запускает webpack в dev-моде, собирает в ./public/assets
    npm run build-client-prod - запускает webpack в prod-моде, собирает в ./public/assets
    npm run build-server - подгатавливает сервер к запуску с помощью babel
    npm run start-dev - запускает сервер в dev-моде с DevMiddleware и HotMiddleware (предварительная сборка через build-client-dev необязательна)
    npm run start-prod - запускает сервер в prod-моду (предварительная сборка через build-dev необязательна)
    npm start - запускает сервер в prod-моде (предварительная сборка через build-client-prod ОБЯЗАТЕЛЬНА)

Сервер запускается на порте 3000 (настраивается в ./server/index.js port)


*** Contacts ***

E-mail: dmitry.banokin@gmail.com