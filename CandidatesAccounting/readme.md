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

    npm run build - запускает сборку клиента и сервера в папку dist
    npm run prod - выполняет build-скрипт и запускает сервер в prod-моде из папки dist
    npm run dev - запускает сервер в dev-моде

Сервер запускается на порте 3000 (настраивается в ./server/index.js port)


*** Contacts ***

E-mail: dmitry.banokin@gmail.com