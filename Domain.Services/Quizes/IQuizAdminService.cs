using Domain.Quizes;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для администрирования
    /// </summary>
    public interface IQuizAdminService
    {
        public Task<string> GetVersion();

        /// <summary>
        /// Добавить викторину
        /// </summary>
        /// <param name="quiz">Викторина</param>
        public Task Add(Quiz quiz);

        /// <summary>
        /// Получить викторину
        /// </summary>
        /// <param name="quiz">Иеднтификатор викторины</param>
        /// <returns>Викторина</returns>
        public Task<Quiz?> Get(int id);

        /// <summary>
        /// Найти викторины по названию
        /// </summary>
        /// <param name="title">Название</param>
        /// <returns>Перечень викторин</returns>
        public Task<FrozenSet<Quiz>> Find(string title);

        /// <summary>
        /// Изменить элемент викторины
        /// </summary>
        /// <param name="quizId">Идентификатор викторины</param>
        /// <param name="itemId">Идентификатор изменяемого элемента</param>
        /// <param name="newItem">Новый элемент</param>
        /// <returns></returns>
        public Task EditQuizItem(int quizId, int itemId, QuizItem newItem);
    }
}
