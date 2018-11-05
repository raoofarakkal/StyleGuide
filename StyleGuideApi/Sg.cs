using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StyleGuide.Db;
using System.Text.RegularExpressions;
using LinqKit;
//using StyleGuide.Db.Table;

//http://www.codeproject.com/Articles/26657/Simple-LINQ-to-SQL-in-C

namespace StyleGuide
{
    public class PagingInfo
    {

        public int TotalPages { 
            get 
            {
                if (TotalRecords > RecordsPerPage)
                {
                    return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalRecords) / Convert.ToDouble(RecordsPerPage)));
                }
                else
                {
                    return 1;
                }
            } 
        }

        public int TotalRecords { get; internal set; }
        private int _currentPage=1;
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value <= 0)
                {
                    _currentPage = 1;
                }
                else
                {
                    _currentPage = value;
                }
            }
        }
        //public int NextPage { get; internal set; }
        //public int PreviousPage { get; internal set; }
        private int _recPerPage = 10;
        public int RecordsPerPage { 
            get
            {
                return _recPerPage;
            }
            set 
            {
                if (value <= 0)
                {
                    _recPerPage = 10;
                }
                else
                {
                    _recPerPage = value;
                }
            }
        }
    }

    namespace SgCategories
    {
        public class CategoryType
        {
            public CategoryType() { }
            internal CategoryType(Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType catType)
            {
                ID = catType.ID;
                Type = catType.Type;
            }
            public long ID { get; set; }
            public string Type { get; set; }
        }

        public class CategoryTypes : List<CategoryType>
        {
            private PagingInfo _pageInfo = null;
            public PagingInfo PageInfo { 
                get 
                {
                    if (_pageInfo == null)
                    {
                        _pageInfo = new PagingInfo();
                        _pageInfo.RecordsPerPage = 10;
                        _pageInfo.CurrentPage = 1;
                    }
                    return _pageInfo;
                } 
            }
        }

        public class Category
        {
            public Category() { }
            internal Category(Db.StyleGuideDb.DbTblEditorialStyleGuideCategory cat) 
            {
                ID = cat.ID;
                Name = cat.Name;
                Notes = cat.Notes;
                Type.ID = cat.CatTypeID;
                Type.Type = cat.CategoryType.Type;
                LMDT = cat.LMDT;
                LMBY = cat.LMBY;
            }

            public long ID { get; set; }
            public string Name { get; set; }
            public string Notes { get; set; }
            public DateTime? LMDT { get; internal set; }
            public string LMBY { get; set; }
            private CategoryType _catType = null;
            public CategoryType Type
            {
                get
                {
                    if (_catType == null)
                    {
                        _catType = new CategoryType();
                    }
                    return _catType;
                }
            }
        }

        public class Categories : List<Category>
        {
            private PagingInfo _pageInfo = null;
            public PagingInfo PageInfo { 
                get 
                {
                    if (_pageInfo == null)
                    {
                        _pageInfo = new PagingInfo();
                        _pageInfo.RecordsPerPage = 10;
                        _pageInfo.CurrentPage = 1;
                    }
                    return _pageInfo;
                } 
            }
        }
    }

    namespace SgEntities
    {
        public class EntityType
        {
            public EntityType() { }
            internal EntityType(Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType entType)
            {
                ID = entType.ID;
                Type = entType.Type;
            }
            public long ID { get; set; }
            public string Type { get; set; }
        }

        public class EntityTypes : List<EntityType>
        {
            private PagingInfo _pageInfo = null;
            public PagingInfo PageInfo { 
                get 
                {
                    if (_pageInfo == null)
                    {
                        _pageInfo = new PagingInfo();
                        _pageInfo.RecordsPerPage = 10;
                        _pageInfo.CurrentPage = 1;
                    }
                    return _pageInfo;
                } 
            }
        }
        
        public class Entity
        {
            public Entity() { }
            internal Entity(Db.StyleGuideDb.DbTblEditorialStyleGuideEntity ent, List<SgCategories.Category> Categories) 
            {
                ID = ent.ID;
                Name = ent.Name;
                Notes = ent.Notes;
                Suspects = ent.Suspects;
                Suggestions = ent.Suggestions;
                Type.ID = ent.EntTypeID;
                Type.Type = ent.EntityType.Type;
                LMDT = ent.LMDT;
                LMBY = ent.LMBY;
                if (Categories != null)
                {
                    foreach (SgCategories.Category cat in Categories)
                    {
                        categories.Add(cat);
                    }
                }
            }

            public long ID { get; set; }
            public string Name { get; set; }
            public string Notes { get; set; }
            public string Suspects { get; set; }
            public string Suggestions { get; set; }
            public DateTime? LMDT { get; internal set; }
            public string LMBY { get; set; }
            private EntityType _entType = null;
            public EntityType Type
            {
                get
                {
                    if (_entType == null)
                    {
                        _entType = new EntityType();
                    }
                    return _entType;
                }
            }
            private IList<SgCategories.Category> _cats = null;
            public IList<SgCategories.Category> categories
            {
                get
                {
                    if (_cats == null)
                    {
                        _cats = new List<SgCategories.Category>();
                    }
                    return _cats;
                }
            }
        }

        public class Entities : List<Entity>
        {
            private PagingInfo _pageInfo = null;
            public PagingInfo PageInfo
            {
                get
                {
                    if (_pageInfo == null)
                    {
                        _pageInfo = new PagingInfo();
                        _pageInfo.RecordsPerPage = 10;
                        _pageInfo.CurrentPage = 1;
                    }
                    return _pageInfo;
                }
            }
        }

        public class EntitiesSearchResultsByCategory : List<EntitiesSearchResultByCategory>
        {

        }

        public class EntitiesSearchResultByCategory
        {
            public SgEntities.EntityType entityType { get; set; }
            public SgEntities.Entities entities { get; set; }

        }
    }



    public class API :IDisposable
    {

        private Db.StyleGuideDb db = null;

        public API()
        {
            db = new Db.StyleGuideDb();
        }

        public void Dispose()
        {
            try
            {
                db.Dispose();
            }
            finally { }
        }




        #region CategoryTypes

        #region Private

        private SgCategories.CategoryTypes transformTablesToCategoryTypes(System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType> results)
        {
            SgCategories.CategoryTypes catTypes = null;
            foreach (Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType tbl in results)
            {
                if (catTypes == null)
                {
                    catTypes = new SgCategories.CategoryTypes();
                }
                catTypes.Add(new SgCategories.CategoryType(tbl));
            }
            return catTypes;
        }

        private void AddCategoryType(SgCategories.CategoryType catType)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType results = null;
            results = (from c in db.TblEditorialStyleGuideCategoryType where c.Type.ToLower() == catType.Type.ToLower() select c).SingleOrDefault();
            if (results == null)
            {
                results = new Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType();
                results.Type = catType.Type;
                db.TblEditorialStyleGuideCategoryType.InsertOnSubmit(results);
                db.TblEditorialStyleGuideCategoryType.Context.SubmitChanges();
            }
            else
            {
                throw new Exception("Failed to add Category Type. Category Type already exist under ID " + results.ID);
            }
        }

        private void UpdateCategoryType(SgCategories.CategoryType catType)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType results = null;
            results = (from c in db.TblEditorialStyleGuideCategoryType where c.ID == catType.ID select c).SingleOrDefault();
            if (results != null)
            {
                Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType t2 = null;
                t2 = (from c in db.TblEditorialStyleGuideCategoryType where c.Type.ToLower() == catType.Type.ToLower() && c.ID != catType.ID select c).SingleOrDefault();
                if (t2 == null)
                {
                    results.Type = catType.Type;
                    db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Failed to update Category type. Category Type already exist under ID " + t2.ID);
                }

            }
            else
            {
                throw new Exception("Failed to update Category Type. Category Type ID " + catType.ID + " does not exist.");
            }

        }

        #endregion

        public void SaveCategoryType(SgCategories.CategoryType catType)
        {
            if (catType.ID > 0)
            {
                UpdateCategoryType(catType);
            }
            else
            {
                AddCategoryType(catType);
            }
        }

        public void DropCategoryType(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType results = null;
            try
            {
                results = (from c in db.TblEditorialStyleGuideCategoryType where c.ID == ID select c).SingleOrDefault();
                db.TblEditorialStyleGuideCategoryType.DeleteOnSubmit(results);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to drop category type ID " + ID + ". " + ex.Message);
            }
            finally { }
        }

        public SgCategories.CategoryTypes getAllCategoryTypes(PagingInfo pageInfo = null, bool orderByType = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType> results = null;
            if (pageInfo != null)
            {
                if (orderByType)
                {
                    results = (from c in db.TblEditorialStyleGuideCategoryType orderby c.Type ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategoryType orderby c.ID ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByType)
                {
                    results = (from c in db.TblEditorialStyleGuideCategoryType orderby c.Type ascending select c);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategoryType orderby c.ID ascending select c);
                }
            }
            return transformTablesToCategoryTypes(results);
        }

        public SgCategories.CategoryType getCategoryTypeByID(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategoryType results = null;
            results = (from c in db.TblEditorialStyleGuideCategoryType where c.ID == ID select c).SingleOrDefault();
            return new SgCategories.CategoryType(results);
        }

        #endregion




        #region Categories

        #region Private

        private SgCategories.Categories transformTablesToCategories(System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results, PagingInfo pageInfo)
        {
            SgCategories.Categories cats = null;
            foreach (Db.StyleGuideDb.DbTblEditorialStyleGuideCategory tbl in results)
            {
                if (cats == null)
                {
                    cats = new SgCategories.Categories();
                    if (pageInfo != null)
                    {
                        cats.PageInfo.RecordsPerPage = pageInfo.RecordsPerPage;
                        cats.PageInfo.TotalRecords = pageInfo.TotalRecords;
                        cats.PageInfo.CurrentPage = pageInfo.CurrentPage;
                    }
                }
                cats.Add(new SgCategories.Category(tbl));
            }
            return cats;
        }

        private void AddCategory(SgCategories.Category cat)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategory results = null;
            results = (from c in db.TblEditorialStyleGuideCategory where c.Name.ToLower() == cat.Name.ToLower() select c).SingleOrDefault();
            if (results == null)
            {
                results = new Db.StyleGuideDb.DbTblEditorialStyleGuideCategory();
                results.Name = cat.Name;
                results.Notes = cat.Notes;
                results.LMBY = cat.LMBY;
                results.LMDT = DateTime.Now;
                results.CategoryType = (from ct in db.TblEditorialStyleGuideCategoryType where ct.ID == cat.Type.ID select ct).SingleOrDefault();
                db.TblEditorialStyleGuideCategory.InsertOnSubmit(results);
                db.TblEditorialStyleGuideCategory.Context.SubmitChanges();
            }
            else
            {
                throw new Exception("Failed to add Category. Category already exist under ID " + results.ID);
            }
        }

        private void UpdateCategory(SgCategories.Category cat)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategory results = null;
            results = (from c in db.TblEditorialStyleGuideCategory where c.ID == cat.ID select c).SingleOrDefault();
            if (results != null)
            {
                Db.StyleGuideDb.DbTblEditorialStyleGuideCategory t2 = null;
                t2 = (from c in db.TblEditorialStyleGuideCategory where c.Name.ToLower() == cat.Name.ToLower() && c.ID != cat.ID select c).SingleOrDefault();
                if (t2 == null)
                {
                    results.Name = cat.Name;
                    results.Notes = cat.Notes;
                    results.LMBY = cat.LMBY;
                    results.LMDT = DateTime.Now;
                    results.CategoryType = (from ct in db.TblEditorialStyleGuideCategoryType where ct.ID == cat.Type.ID select ct).SingleOrDefault();
                    db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Failed to update Category. Category already exist under ID " + t2.ID );
                }

            }
            else
            {
                throw new Exception("Failed to update Category. Category ID " + cat.ID + " does not exist.");
            }

        }

        #endregion

        public void SaveCategory(SgCategories.Category cat)
        {
            if (cat.ID > 0)
            {
                UpdateCategory(cat);
            }
            else
            {
                AddCategory(cat);
            }
        }

        public void DropCategory(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategory results = null;
            try
            {
                results = (from c in db.TblEditorialStyleGuideCategory where c.ID == ID select c).SingleOrDefault();
                db.TblEditorialStyleGuideCategory.DeleteOnSubmit(results);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to drop category ID " + ID + ". " + ex.Message);
            }
            finally { }
        }

        public SgCategories.Categories getAllCategories(PagingInfo pageInfo = null, bool orderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from c in db.TblEditorialStyleGuideCategory select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                } 
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.Name ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.ID ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.Name ascending select c);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.ID ascending select c);
                }
            }
            return transformTablesToCategories(results,pageInfo);
        }

        public SgCategories.Categories getAllCategoriesNameStartsWith(string StartsWith ,PagingInfo pageInfo = null, bool orderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from c in db.TblEditorialStyleGuideCategory where c.Name.StartsWith(StartsWith) select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                } 
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.StartsWith(StartsWith) orderby c.Name ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.StartsWith(StartsWith) orderby c.ID ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.StartsWith(StartsWith) orderby c.Name ascending select c);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.StartsWith(StartsWith) orderby c.ID ascending select c);
                }
            }
            return transformTablesToCategories(results,pageInfo);
        }
        
        public SgCategories.Categories getAllCategoriesNameContains(string Contains, PagingInfo pageInfo = null, bool orderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from c in db.TblEditorialStyleGuideCategory where c.Name.Contains(Contains) select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                }
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.Contains(Contains) orderby c.Name ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.Contains(Contains) orderby c.ID ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.Contains(Contains) orderby c.Name ascending select c);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory where c.Name.Contains(Contains) orderby c.ID ascending select c);
                }
            }
            return transformTablesToCategories(results, pageInfo);
        }

        public SgCategories.Categories getAllCategoriesOrderByType(PagingInfo pageInfo = null, bool SubOrderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from c in db.TblEditorialStyleGuideCategory select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                }
                if (SubOrderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory  orderby c.CategoryType.Type, c.Name ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.CategoryType.Type, c.ID ascending select c).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (SubOrderByName)
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.CategoryType.Type, c.Name ascending select c);
                }
                else
                {
                    results = (from c in db.TblEditorialStyleGuideCategory orderby c.CategoryType.Type, c.ID ascending select c);
                }
            }
            return transformTablesToCategories(results, pageInfo);
        }

        /// <summary>
        /// Take all the words in the input string and separate them.
        /// </summary>
        private string[] SplitWords(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return new string[] { };
            }
            //
            // Split on all non-word characters.
            // ... Returns an array of all the words.
            //
            return Regex.Split(s, @"\W+");
            // @      special verbatim string syntax
            // \W+    one or more non-word characters together
        }

        public SgCategories.Categories getAllCategoriesContainsEntitiesOrderByType(string EntityNameKeywords, bool SubOrderByName = false)
        {
            string[] keywords = SplitWords(EntityNameKeywords);
            var predicate = Db.StyleGuideDb.DbTblEditorialStyleGuideEntity.ContainsInNameAndNotes(keywords);

            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideCategory> results = null;
            if (SubOrderByName)
            {
                results = (
                    from c in db.TblEditorialStyleGuideCategory.AsExpandable()
                    where (
                        from ec in db.TblEditorialStyleGuideEntityCategories.AsExpandable()
                        where
                        (
                            from e in db.TblEditorialStyleGuideEntity.Where(predicate)
                            select e.ID
                        ).Contains(ec.EntID)
                        select ec.CatID
                    ).Contains(c.ID)
                    orderby c.CategoryType.Type, c.Name ascending 
                    select c
                );
            }
            else
            {
                results = (
                    from c in db.TblEditorialStyleGuideCategory.AsExpandable()
                    where (
                        from ec in db.TblEditorialStyleGuideEntityCategories.AsExpandable()
                        where
                        (
                            from e in db.TblEditorialStyleGuideEntity.Where(predicate)
                            select e.ID
                        ).Contains(ec.EntID)
                        select ec.CatID
                    ).Contains(c.ID)
                    orderby c.CategoryType.Type, c.ID ascending 
                    select c
                );
            }
            return transformTablesToCategories(results, null);
        }


        public SgCategories.Category getCategoryByID(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideCategory results = null;
            results = (from c in db.TblEditorialStyleGuideCategory where c.ID == ID select c).SingleOrDefault();
            return new SgCategories.Category(results);
        }

        #endregion





        #region EntityTypes

        #region Private

        private SgEntities.EntityTypes transformTablesToEntityTypes(System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType> results)
        {
            SgEntities.EntityTypes entTypes = null;
            foreach (Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType tbl in results)
            {
                if (entTypes == null)
                {
                    entTypes = new SgEntities.EntityTypes();
                }
                entTypes.Add(new SgEntities.EntityType(tbl));
            }
            return entTypes;
        }

        private void AddEntityType(SgEntities.EntityType entType)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType results = null;
            results = (from e in db.TblEditorialStyleGuideEntityType where e.Type.ToLower() == entType.Type.ToLower() select e).SingleOrDefault();
            if (results == null)
            {
                results = new Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType();
                results.Type = entType.Type;
                db.TblEditorialStyleGuideEntityType.InsertOnSubmit(results);
                db.TblEditorialStyleGuideEntityType.Context.SubmitChanges();
            }
            else
            {
                throw new Exception("Failed to add Entity Type. Entity Type already exist under ID " + results.ID);
            }
        }

        private void UpdateEntityType(SgEntities.EntityType entType)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType results = null;
            results = (from e in db.TblEditorialStyleGuideEntityType where e.ID == entType.ID select e).SingleOrDefault();
            if (results != null)
            {
                Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType t2 = null;
                t2 = (from e in db.TblEditorialStyleGuideEntityType where e.Type.ToLower() == entType.Type.ToLower() && e.ID != entType.ID select e).SingleOrDefault();
                if (t2 == null)
                {
                    results.Type = entType.Type;
                    db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Failed to update Entity type. Entity Type already exist under ID " + t2.ID);
                }

            }
            else
            {
                throw new Exception("Failed to update Entity Type. Entity Type ID " + entType.ID + " does not exist.");
            }

        }

        #endregion

        public void SaveEntityType(SgEntities.EntityType entType)
        {
            if (entType.ID > 0)
            {
                UpdateEntityType(entType);
            }
            else
            {
                AddEntityType(entType);
            }
        }

        public void DropEntityType(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType results = null;
            try
            {
                results = (from e in db.TblEditorialStyleGuideEntityType where e.ID == ID select e).SingleOrDefault();
                db.TblEditorialStyleGuideEntityType.DeleteOnSubmit(results);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to drop Entity type ID " + ID + ". " + ex.Message);
            }
            finally { }
        }

        public SgEntities.EntityTypes getAllEntityTypes(PagingInfo pageInfo = null, bool orderByType = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType> results = null;
            if (pageInfo != null)
            {
                if (orderByType)
                {
                    results = (from e in db.TblEditorialStyleGuideEntityType orderby e.Type ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntityType orderby e.ID ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByType)
                {
                    results = (from e in db.TblEditorialStyleGuideEntityType orderby e.Type ascending select e);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntityType orderby e.ID ascending select e);
                }
            }
            return transformTablesToEntityTypes(results);
        }

        public SgEntities.EntityType getEntityTypeByID(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityType results = null;
            results = (from e in db.TblEditorialStyleGuideEntityType where e.ID == ID select e).SingleOrDefault();
            return new SgEntities.EntityType(results);
        }

        #endregion





        #region Entities

        #region Private

        private SgEntities.Entities transformTablesToEntities(System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntity> results,PagingInfo pageInfo)
        {
            SgEntities.Entities ents = null;
            foreach (Db.StyleGuideDb.DbTblEditorialStyleGuideEntity tbl in results)
            {
                if (ents == null)
                {
                    ents = new SgEntities.Entities();
                    if (pageInfo != null)
                    {
                        ents.PageInfo.RecordsPerPage = pageInfo.RecordsPerPage;
                        ents.PageInfo.TotalRecords = pageInfo.TotalRecords;
                        ents.PageInfo.CurrentPage = pageInfo.CurrentPage;
                    }
                }
                ents.Add(new SgEntities.Entity(tbl, getEntityCategories(tbl.ID)));
            }
            return ents;
        }

        private void AddEntity(SgEntities.Entity ent)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntity results = null;
            results = (from e in db.TblEditorialStyleGuideEntity where e.Name.ToLower() == ent.Name.ToLower() select e).SingleOrDefault();
            if (results == null)
            {
                results = new Db.StyleGuideDb.DbTblEditorialStyleGuideEntity();
                results.Name = ent.Name;
                results.Notes = ent.Notes;
                results.Suspects = ent.Suspects;
                results.Suggestions = ent.Suggestions;
                results.LMBY = ent.LMBY;
                results.LMDT = DateTime.Now;
                results.EntityType = (from et in db.TblEditorialStyleGuideEntityType where et.ID == ent.Type.ID select et).SingleOrDefault();
                db.TblEditorialStyleGuideEntity.InsertOnSubmit(results);
                db.TblEditorialStyleGuideEntity.Context.SubmitChanges();
            }
            else
            {
                throw new Exception("Failed to add Entity. Entity already exist under ID " + results.ID);
            }
        }

        private void UpdateEntity(SgEntities.Entity ent)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntity results = null;
            results = (from e in db.TblEditorialStyleGuideEntity where e.ID == ent.ID select e).SingleOrDefault();
            if (results != null)
            {
                Db.StyleGuideDb.DbTblEditorialStyleGuideEntity t2 = null;
                t2 = (from e in db.TblEditorialStyleGuideEntity where e.Name.ToLower() == ent.Name.ToLower() && e.ID != ent.ID select e).SingleOrDefault();
                if (t2 == null)
                {
                    results.Name = ent.Name;
                    results.Notes = ent.Notes;
                    results.Suspects = ent.Suspects;
                    results.Suggestions = ent.Suggestions;
                    results.LMBY = ent.LMBY;
                    results.LMDT = DateTime.Now;
                    results.EntityType = (from et in db.TblEditorialStyleGuideEntityType where et.ID == ent.Type.ID select et).SingleOrDefault();
                    db.SubmitChanges();
                }
                else
                {
                    throw new Exception("Failed to update Entity. Entity already exist under ID " + t2.ID);
                }

            }
            else
            {
                throw new Exception("Failed to update Entity. Entity ID " + ent.ID + " does not exist.");
            }

        }

        #endregion

        public void SaveEntity(SgEntities.Entity ent)
        {
            if (ent.ID > 0)
            {
                UpdateEntity(ent);
            }
            else
            {
                AddEntity(ent);
            }
        }

        public void DropEntity(long ID)
        {
            try
            {
                System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories> cats = null;
                cats = (from ec in db.TblEditorialStyleGuideEntityCategories where ec.EntID == ID select ec);
                db.TblEditorialStyleGuideEntityCategories.DeleteAllOnSubmit(cats);

                Db.StyleGuideDb.DbTblEditorialStyleGuideEntity ent = null;
                ent = (from e in db.TblEditorialStyleGuideEntity where e.ID == ID select e).SingleOrDefault();
                db.TblEditorialStyleGuideEntity.DeleteOnSubmit(ent);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to drop Entity ID " + ID + ". " + ex.Message);
            }
            finally { }
        }

        public SgEntities.Entities getAllEntities(PagingInfo pageInfo = null, bool orderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntity> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from e in db.TblEditorialStyleGuideEntity select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                }
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity orderby e.Name ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity orderby e.ID ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity orderby e.Name ascending select e);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity orderby e.ID ascending select e);
                }
            }
            return transformTablesToEntities(results,pageInfo);
        }

        public SgEntities.Entities getAllEntitiesNameStartsWith(string Startwith, PagingInfo pageInfo = null, bool orderByName = false)
        {
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntity> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from e in db.TblEditorialStyleGuideEntity where e.Name.StartsWith(Startwith) select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                }
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity where e.Name.StartsWith(Startwith) orderby e.Name ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity where e.Name.StartsWith(Startwith) orderby e.ID ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity where e.Name.StartsWith(Startwith) orderby e.Name ascending select e);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity where e.Name.StartsWith(Startwith) orderby e.ID ascending select e);
                }
            }
            return transformTablesToEntities(results,pageInfo);
        }

        public SgEntities.Entities getAllEntitiesNameContains(string Contains, PagingInfo pageInfo = null, bool orderByName = false)
        {
            string[] keywords = SplitWords(Contains);
            var predicate = Db.StyleGuideDb.DbTblEditorialStyleGuideEntity.ContainsInName(keywords);

            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntity> results = null;
            if (pageInfo != null)
            {
                pageInfo.TotalRecords = (from e in db.TblEditorialStyleGuideEntity where e.Name.Contains(Contains) select new { }).Count();
                if (pageInfo.CurrentPage > pageInfo.TotalPages)
                {
                    pageInfo.CurrentPage = pageInfo.TotalPages;
                }
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity.Where(predicate) orderby e.Name ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity.Where(predicate) orderby e.ID ascending select e).Skip(pageInfo.RecordsPerPage * (pageInfo.CurrentPage - 1)).Take(pageInfo.RecordsPerPage);
                }
            }
            else
            {
                if (orderByName)
                {
                    results = (from e in db.TblEditorialStyleGuideEntity.Where(predicate) orderby e.Name ascending select e);
                }
                else
                {
                    results = (from e in db.TblEditorialStyleGuideEntity.Where(predicate) orderby e.ID ascending select e);
                }
            }
            return transformTablesToEntities(results, pageInfo);
        }

        public SgEntities.Entity getEntityByID(long ID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntity ent = null;
            ent = (from e in db.TblEditorialStyleGuideEntity where e.ID == ID select e).SingleOrDefault();
            return new SgEntities.Entity(ent,getEntityCategories(ent.ID));
        }

        public int getEntityCountModifiedLaterthan(long catID, DateTime date)
        {
            int count = 0;
            count = (from e in db.TblEditorialStyleGuideEntity
                     where (
                         from ec in db.TblEditorialStyleGuideEntityCategories
                         where ec.CatID == catID
                         select ec.EntID
                     ).Contains(e.ID)
                     && e.LMDT > date
                     select e.ID
            ).Count();
            return count;
        }

        #endregion

        #region Entity Search Module Related

        public SgEntities.EntitiesSearchResultsByCategory getEntitiesForSearchModule(long categoryID,string keyword)
        {
            string[] keywords = SplitWords(keyword);
            var predicate = Db.StyleGuideDb.DbTblEditorialStyleGuideEntity.ContainsInNameAndNotes(keywords);

            SgEntities.EntitiesSearchResultsByCategory results = null;

            //get Entity Types
            System.Linq.IQueryable<StyleGuideDb.DbTblEditorialStyleGuideEntityType> DbEntTypes = null;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                /*
                DbEntTypes = (
                    from et in
                        db.TblEditorialStyleGuideEntityType
                    where
                        (
                            (
                                from e in
                                    db.TblEditorialStyleGuideEntity
                                where (e.Name.Contains(keyword)
                                    || e.Notes.Contains(keyword)
                                )

                                &&
                                (
                                    (
                                        from ec in
                                            db.TblEditorialStyleGuideEntityCategories
                                        where ec.CatID == categoryID
                                        select ec.EntID
                                    ).Distinct()
                                ).Contains(e.ID)
                                select e.EntTypeID
                            ).Distinct()
                        ).Contains(et.ID)
                    orderby et.Type
                    select et
                );
                */

                DbEntTypes = (from et in db.TblEditorialStyleGuideEntityType
                              join e in db.TblEditorialStyleGuideEntity.Where(predicate) on et.ID equals e.EntTypeID
                              join ec in db.TblEditorialStyleGuideEntityCategories on e.ID equals ec.EntID
                              where ec.CatID == categoryID
                              select et).Distinct().OrderBy(t => t.Type);
            }
            else
            {
                /*
                DbEntTypes = (
                    from et in
                        db.TblEditorialStyleGuideEntityType
                    where
                        (
                            (
                                from e in
                                    db.TblEditorialStyleGuideEntity
                                where
                                (
                                    (
                                        from ec in
                                            db.TblEditorialStyleGuideEntityCategories
                                        where ec.CatID == categoryID
                                        select ec.EntID
                                    ).Distinct()
                                ).Contains(e.ID)
                                select e.EntTypeID
                            ).Distinct()
                        ).Contains(et.ID)
                    orderby et.Type
                    select et
                );
                */

                DbEntTypes = (from et in db.TblEditorialStyleGuideEntityType
                              join e in db.TblEditorialStyleGuideEntity on et.ID equals e.EntTypeID
                              join ec in db.TblEditorialStyleGuideEntityCategories on e.ID equals ec.EntID
                              where ec.CatID == categoryID
                              select et).Distinct().OrderBy(t => t.Type);
            }

            StyleGuide.SgEntities.EntityTypes entTypes = transformTablesToEntityTypes(DbEntTypes);
            if (entTypes != null)
            {
                int max = entTypes.Count();
                foreach (StyleGuide.SgEntities.EntityType entType in entTypes)
                {
                    SgEntities.EntitiesSearchResultByCategory elm = new SgEntities.EntitiesSearchResultByCategory();
                    elm.entityType = entType;

                    //get Entities
                    System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntity> DbEnts = null;
                    if(!string.IsNullOrWhiteSpace(keyword))
                    {
                        /*
                        DbEnts = (
                             from e in
                                 db.TblEditorialStyleGuideEntity
                             where e.EntTypeID == entType.ID &&
                                (e.Name.Contains(keyword)
                                 || e.Notes.Contains(keyword)
                                 )
                                &&
                             (
                                 (
                                     from ec in
                                         db.TblEditorialStyleGuideEntityCategories
                                     where ec.CatID == categoryID
                                     select ec.EntID
                                 ).Distinct()
                             ).Contains(e.ID)
                             orderby e.Name
                             select e
                        );
                        */

                        DbEnts = (from e in db.TblEditorialStyleGuideEntity.Where(predicate)
                                  join et in db.TblEditorialStyleGuideEntityType on e.EntTypeID equals et.ID
                                  join ec in db.TblEditorialStyleGuideEntityCategories on e.ID equals ec.EntID
                                  where e.EntTypeID == entType.ID && ec.CatID == categoryID
                                  select e).Distinct().OrderBy(n => n.Name);
                    }
                    else
                    {
                        /*
                        DbEnts = (
                             from e in
                                 db.TblEditorialStyleGuideEntity
                             where e.EntTypeID == entType.ID &&
                             (
                                 (
                                     from ec in
                                         db.TblEditorialStyleGuideEntityCategories
                                     where ec.CatID == categoryID
                                     select ec.EntID
                                 ).Distinct()
                             ).Contains(e.ID)
                             orderby e.Name
                             select e
                        );
                        */

                        DbEnts = (from e in db.TblEditorialStyleGuideEntity
                                  join et in db.TblEditorialStyleGuideEntityType on e.EntTypeID equals et.ID
                                  join ec in db.TblEditorialStyleGuideEntityCategories on e.ID equals ec.EntID
                                  where e.EntTypeID == entType.ID && ec.CatID == categoryID
                                  select e).Distinct().OrderBy(n => n.Name);
                    }

                    SgEntities.Entities ents = transformTablesToEntities(DbEnts, null);
                    elm.entities = ents;
                    if (results == null)
                    {
                        results = new SgEntities.EntitiesSearchResultsByCategory();
                    }
                    results.Add(elm);
                }
            }
            return results;
        }

        #endregion

        #region Entity StyleGuide Dictionary Related


        #endregion 

        #region EntityCategory Relation

        public void AddCategoryToEntity(long catID, long entID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories entCats = null;
            entCats = (from ec in db.TblEditorialStyleGuideEntityCategories where ec.CatID == catID && ec.EntID == entID select ec).SingleOrDefault();
            if (entCats == null)
            {
                entCats = new Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories();
                entCats.EntID = entID;
                entCats.CatID = catID;
                db.TblEditorialStyleGuideEntityCategories.InsertOnSubmit(entCats);
                db.TblEditorialStyleGuideEntityCategories.Context.SubmitChanges();
            }
            else
            {
                throw new Exception("Failed to add Category to Entity. Category already exist in Entity");
            }
        }

        public void RemoveCategoryFromEntity(long catID,long entID)
        {
            Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories entCats = null;
            try
            {
                entCats = (from ec in db.TblEditorialStyleGuideEntityCategories where ec.EntID == entID && ec.CatID == catID select ec).SingleOrDefault();
                db.TblEditorialStyleGuideEntityCategories.DeleteOnSubmit(entCats);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { }
        }

        public SgCategories.Categories getEntityCategories(long entID)
        {
            SgCategories.Categories cats = null;
            System.Linq.IQueryable<Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories> entCats = null;
            entCats = (from ec in db.TblEditorialStyleGuideEntityCategories where ec.EntID == entID select ec);
            foreach (Db.StyleGuideDb.DbTblEditorialStyleGuideEntityCategories entCat in entCats)
            {
                SgCategories.Category cat = getCategoryByID(entCat.CatID);
                if (cats == null)
                {
                    cats = new SgCategories.Categories();
                }
                cats.Add(cat);
            }
            return cats;
        }

        #endregion


    }

   

}
