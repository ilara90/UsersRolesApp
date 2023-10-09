using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using UsersRolesApp.Models;

namespace UsersRolesApp.Controllers
{
    /// <summary>
    /// Операции для управления списком пользователей
    /// </summary>
    /// <returns></returns>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        ApplicationContext db;

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public UserController(ApplicationContext context)
        {
            db = context;
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <param name="filter">Данные о сортировке</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpPost("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(UserFilter filter)
        {
            //фильтрация
            IQueryable<User>? users = db.Users.Include(r => r.Roles);

            if (!string.IsNullOrEmpty(filter.NameUser))
            {
                users = users.Where(p => p.Name!.Contains(filter.NameUser));
            }

            if (!string.IsNullOrEmpty(filter.Email))
            {
                users = users.Where(p => p.Email!.Contains(filter.Email));
            }

            if(filter.AgeFrom != null)
            {
                users = users.Where(p => p.Age > filter.AgeFrom);
            }

            if (filter.AgeTo != null)
            {
                users = users.Where(p => p.Age < filter.AgeTo);
            }

            if (filter.RoleId != null)
            {
                users = users.Where(p => p.Roles.Any(r => r.Id == filter.RoleId));
            }

            // сортировка
            if (filter?.Paging?.SortDirection == SortDirection.Asc)
            {
                users = users.OrderBy(GetUserSortFunction(filter.Paging.SortingField));
            }
            else
            {
                users = users.OrderByDescending(GetUserSortFunction(filter.Paging.SortingField));
            }

            // пагинация
            var count = await users.CountAsync();
            var items = await users.Skip((filter.Paging.PageNumber - 1) * filter.Paging.PageSize).Take(filter.Paging.PageSize).ToListAsync();

            // формируем модель представления
            UsersResponse usersResponse = new UsersResponse(
                items,
                new PageResponse(count, filter.Paging.PageNumber, filter.Paging.PageSize)
            );

            return Ok(usersResponse);
        }

        private Expression<Func<User, object>> GetUserSortFunction(SortUserField userField) =>
        userField switch
        {
            SortUserField.NameUser => (User user) => user.Name,
            SortUserField.Email => (User user) => user.Email,
            SortUserField.Age => (User user) => user.Age
        };



        /// <summary>
        /// Получение пользователя по Id и всех его ролей
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpGet]
        [Route("GetUserById/{id:int}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user != null)
            { 
                return Ok(user);
            }

            return NotFound();
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        [HttpPost("AddUser")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddUser(User user)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(errors);
            }

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Добавление новых ролей пользователю.
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="roles">Список ролей, которые нужно добавить пользователю</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Ошибка API</response>
        /// <returns></returns>
        [HttpPost("AddUserRoles")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddUserRoles([FromRoute] int id, List<Role> roles)
        {
            if (roles.Count() < 1)
            {
                return BadRequest("Выберите хотя бы одну роль");
            }

            var user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if(user == null)
            {
                return BadRequest("Пользователь не найден");
            }

            List<UsersRoles> userRolesNew = new List<UsersRoles>();

            foreach (Role role in roles)
            {
                UsersRoles userRole = new UsersRoles();
                userRole.UserId = id;
                userRole.RoleId = role.Id;
                userRolesNew.Add(userRole);
            }

            await db.UsersRoles.AddRangeAsync(userRolesNew);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Обновление информации о пользователе по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="updateUser">Обновленные данные пользователя</param>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser/{id:int}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, User updateUser)
        {
            var user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Age = updateUser.Age;

            db.Update(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Удаление пользователя по Id 
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns></returns>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="404">Пользователь не найден</response>
        [HttpDelete]
        [Route("DeleteUser/{id:int}")]
        [ProducesResponseType(typeof(User), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var user = await db.Users.Include(r => r.Roles).FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Проверка уникальности email
        /// </summary>
        /// <param name="email">Адрес электроной почты, который требуется проверить на уникальность</param>
        /// <returns></returns>
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckEmail(string email)
        {
            var emailCurrent = db.Users.Any(e => e.Email == email);

            return Json(emailCurrent);
        }
    }
}
