using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;
using LinqKit;

//http://www.codeproject.com/Articles/26657/Simple-LINQ-to-SQL-in-C

namespace StyleGuide.Db
{
    internal class StyleGuideDb : DataContext
    {
        internal StyleGuideDb() : base(StyleGuide.SgConfig.DbConString()) { }

        public Table<DbTblEditorialStyleGuideCategoryType> TblEditorialStyleGuideCategoryType = null;
        public Table<DbTblEditorialStyleGuideCategory> TblEditorialStyleGuideCategory = null;
        public Table<DbTblEditorialStyleGuideEntityType> TblEditorialStyleGuideEntityType = null;
        public Table<DbTblEditorialStyleGuideEntity> TblEditorialStyleGuideEntity = null;
        public Table<DbTblEditorialStyleGuideEntityCategories> TblEditorialStyleGuideEntityCategories = null;


        [Table(Name = "EditorialStyleGuideCategoryType")]
        public class DbTblEditorialStyleGuideCategoryType 
        {
            [Column(AutoSync = AutoSync.OnInsert, DbType = "Numeric(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public long ID { get; set; }

            [Column(DbType = "nvarchar(50)")]
            public string Type { get; set; }
        }

        [Table(Name = "EditorialStyleGuideCategory")]
        public class DbTblEditorialStyleGuideCategory
        {
            [Column(AutoSync = AutoSync.OnInsert, DbType = "Numeric(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public long ID { get; set; }

            [Column(DbType = "nvarchar(100)")]
            public string Name { get; set; }

            [Column(DbType = "nvarchar(1000)")]
            public string Notes { get; set; }

            [Column(DbType = "Numeric(18,0)")]
            public long CatTypeID { get; set; }

            [Column(DbType = "datetime", CanBeNull=true)]
            public DateTime? LMDT { get; set; }

            [Column(DbType = "nvarchar(50)")]
            public string LMBY { get; set; }
            
            //Foreign Key  
            private EntityRef<DbTblEditorialStyleGuideCategoryType> _CT = new EntityRef<DbTblEditorialStyleGuideCategoryType>();
            [Association(Name = "FK_EditorialStyleGuideCategory_EditorialStyleGuideCategoryType", Storage = "_CT", ThisKey = "CatTypeID", IsForeignKey = true)]
            public DbTblEditorialStyleGuideCategoryType CategoryType { get { return this._CT.Entity; } set { this._CT.Entity = value; } }

        }

        [Table(Name = "EditorialStyleGuideEntityType")]
        public class DbTblEditorialStyleGuideEntityType
        {
            [Column(AutoSync = AutoSync.OnInsert, DbType = "Numeric(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public long ID { get; set; }

            [Column(DbType = "nvarchar(50)")]
            public string Type { get; set; }
        }

        [Table(Name = "EditorialStyleGuideEntity")]
        public class DbTblEditorialStyleGuideEntity
        {
            [Column(AutoSync = AutoSync.OnInsert, DbType = "Numeric(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public long ID { get; set; }

            [Column(DbType = "nvarchar(100)")]
            public string Name { get; set; }

            [Column(DbType = "nvarchar(1000)")]
            public string Notes { get; set; }

            [Column(DbType = "nvarchar(1000)")]
            public string Suspects { get; set; }

            [Column(DbType = "nvarchar(1000)")]
            public string Suggestions { get; set; }
            
            [Column(DbType = "Numeric(18,0)")]
            public long EntTypeID { get; set; }

            [Column(DbType = "datetime", CanBeNull=true)]
            public DateTime? LMDT { get; set; }

            [Column(DbType = "nvarchar(50)")]
            public string LMBY { get; set; }

            //Foreign Key  
            private EntityRef<DbTblEditorialStyleGuideEntityType> _ET = new EntityRef<DbTblEditorialStyleGuideEntityType>();
            [Association(Name = "FK_EditorialStyleGuideEntity_EditorialStyleGuideEntityType", Storage = "_ET", ThisKey = "EntTypeID", IsForeignKey = true)]
            public DbTblEditorialStyleGuideEntityType EntityType { get { return this._ET.Entity; } set { this._ET.Entity = value; } }

            public static Expression<Func<DbTblEditorialStyleGuideEntity, bool>> ContainsInNameAndNotes(
                params string[] keywords)
            {
                var predicate = PredicateBuilder.False<DbTblEditorialStyleGuideEntity>();
                foreach (string keyword in keywords)
                {
                    string temp = keyword;
                    predicate = predicate.Or(p => p.Name.Contains(temp));
                    predicate = predicate.Or(p => p.Notes.Contains(temp));
                }

                return predicate;
            }

            public static Expression<Func<DbTblEditorialStyleGuideEntity, bool>> ContainsInName(
                params string[] keywords)
            {
                var predicate = PredicateBuilder.False<DbTblEditorialStyleGuideEntity>();
                foreach (string keyword in keywords)
                {
                    string temp = keyword;
                    predicate = predicate.Or(p => p.Name.Contains(temp));
                }

                return predicate;
            }
        }

        [Table(Name = "EditorialStyleGuideEntityCategories")]
        public class DbTblEditorialStyleGuideEntityCategories
        {
            [Column(AutoSync = AutoSync.OnInsert, DbType = "Numeric(18,0) NOT NULL IDENTITY", IsPrimaryKey = true, IsDbGenerated = true)]
            public long ID { get; set; }

            [Column(DbType = "Numeric(18,0)")]
            public long EntID { get; set; }

            [Column(DbType = "Numeric(18,0)")]
            public long CatID { get; set; }

            //Foreign Key  
            private EntityRef<DbTblEditorialStyleGuideEntity> _E = new EntityRef<DbTblEditorialStyleGuideEntity>();
            [Association(Name = "FK_EditorialStyleGuideEntityCategories_EditorialStyleGuideEntity", Storage = "_E", ThisKey = "EntID", IsForeignKey = true)]
            public DbTblEditorialStyleGuideEntity Entity { get { return this._E.Entity; } set { this._E.Entity = value; } }

            //Foreign Key  
            private EntityRef<DbTblEditorialStyleGuideCategory> _C = new EntityRef<DbTblEditorialStyleGuideCategory>();
            [Association(Name = "FK_EditorialStyleGuideEntityCategories_EditorialStyleGuideCategory", Storage = "_C", ThisKey = "CatID", IsForeignKey = true)]
            public DbTblEditorialStyleGuideCategory Category { get { return this._C.Entity; } set { this._C.Entity = value; } }

        }

    }

}

