using System;
using System.Linq.Expressions;
using ABSA.PhoneBook.Domain.Entities;
namespace ABSA.PhoneBook.API.Application.Utilities
{
    public class SearchExpressionHelper
    {
        public static Expression<Func<TEntity,bool>> GetSearchExpression<TEntity>(string searchCriteria) where TEntity : Domain.Entities.PhoneBook
        {
            return x => searchCriteria == null || (x.Name.ToLower().Contains(searchCriteria.ToLower()));
        }

        public static Expression<Func<TEntity, bool>> GetSearchEntryExpression<TEntity>(string searchCriteria,int phoneBookId) where TEntity : Domain.Entities.PhoneBookEntry
        {
            return x => (searchCriteria == null || (x.Name.ToLower().Contains(searchCriteria.ToLower()) 
                         || x.PhoneNumber.Contains(searchCriteria))) && x.PhoneBookId == phoneBookId;
        }
    }
}