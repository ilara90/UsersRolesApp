Создан ASP.NET Core Web API, который предоставляет CRUD (Create, Read, Update, Delete) операции для управления списком пользователей. У каждого пользователя есть следующие атрибуты: Id, Имя (Name), Возраст (Age), Email и связанная сущность "Роль" (Role) со значениями (User, Admin, Support, SuperAdmin), где у одного юзера может несколько ролей.
Выполнено:
-Создан новый проект ASP.NET Core Web API с использованием языка C# и .NET Core.
-Создан модель данных для пользователя (User) с атрибутами Id, Name, Age, Email и связанной сущностью "Роль" (Role).
-Созданконтроллер (UserController) с методами для выполнения следующих операций:
1.Получение списка всех пользователей(реалезована пагинация, сортировка и фильтрация по каждому атрибуту в модели User).
2.Получение пользователя по Id и всех его ролей.
3. Добавление новой роли пользователю.
4. Создание нового пользователя.
5. Обновление информации о пользователе по Id.
6. Удаление пользователя по Id.
Добавлена в контроллер бизнес-логику для валидации данных:
1. Проверка наличия обязательных полей (Имя, Возраст, Email).
2. Проверка уникальности Email.
3. Проверка возраста на положительное число.
-Используйтся Entity Framework Core для доступа к данным и сохранения пользователей и ролей в базе данных.
-Создана миграция для создания необходимой таблицы в базе данных.
-Добавлена обработка ошибок и возврат соответствующих статусных кодов HTTP (например, 404 при отсутствии пользователя).
-Задокументирован API с использованием инструментов Swagger.
-Добавлена аутентификация и авторизация к API с использованием JWT-токенов.