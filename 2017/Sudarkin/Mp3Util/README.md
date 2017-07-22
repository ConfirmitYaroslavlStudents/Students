### Изменения

 1. Библиотека TagLib полностью инкапсулирована в классах Mp3File, FileAbstraction
 2. Основная логика программы вынесена в класс Processor
 3. Mp3File имеет метод MoveTo(path) для перемещения (переименования) файла
 4. Проект Mp3UtilConsole разделен на Mp3UtilConsole и Mp3UtilLib
 5. ArgumentManager -> ArgumentParser
 6. Удален интерфейс ILogger
 7. Удален файл Dummy.mp3. GetDummyMp3() теперь возвращает заготовленный массив байтов пустого mp3 файла