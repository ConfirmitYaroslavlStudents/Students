CANDIDATE ACCOUNTING README


*** MongoDB ***

Необходимо для работы приложения.

Используется бесплатная Community версия: https://www.mongodb.com/download-center#community
Параметры установки по умолчанию.

1. Приложение ожидает сервер на порте 27017 (настраивается в server/mongoose.js connectionURL).
2. Необходимо создать базу данных с названием "CandidateAccounting" без пароля (настраивается в server/mongoose.js connectionURL)
3. В базе данных должно быть три коллекции:
    a. "accounts"
    b. "candidates"
    c. "tags"
4. Подключение к базе данных осуществляется автоматически при запуске сервера


*** Scripts ***

npm run build-dev - запускает webpack в dev-моде, собирает в ./public/assets
npm run build-prod - запускает webpack в prod-моде, собирает в ./public/assets
npm run start-dev - запускает сервер в dev-моде с DevMiddleware и HotMiddleware (предварительная сборка через build-dev необязательна)
npm start - запускает сервер в prod-моде (предварительная сборка через build-prod ОБЯЗАТЕЛЬНА)

Сервер запускается на порте 3000 (настраивается в ./server/server.js port)


*** Contacts ***

E-mail: dmitry.banokin@gmail.com