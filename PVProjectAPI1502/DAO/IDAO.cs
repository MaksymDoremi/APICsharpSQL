using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PVProjectAPI1502.DAO
{
    public interface IDAO<T> where T : IBaseClass
    {
        #region CRUD
        /// <summary>
        /// Inserts element into database
        /// </summary>
        /// <param name="element"></param>
        /// <returns>True if succeeded</returns>
        bool Insert(T element);
        /// <summary>
        /// Gets element by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T element</returns>
        T? GetByID(int id);
        /// <summary>
        /// Gets all elements from database
        /// </summary>
        /// <returns>Datatable filled with elements</returns>
        DataTable? GetAllDatatable();
        /// <summary>
        /// Gets list of all elements from database
        /// </summary>
        /// <returns>List of elements</returns>
        List<T>? GetAllList();
        /// <summary>
        /// Updates element by ID
        /// </summary>
        /// <param name="element"></param>
        /// <returns>True if succeede</returns>
        bool Update(T element);
        /// <summary>
        /// Deletes element by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True if succeeded</returns>
        bool Delete(int id);
        #endregion
        #region Exports to JSON
        /// <summary>
        /// Exports datatable to JSON, default path is "entity.json"
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>JSON string</returns>
        string ExportToJSON(DataTable dataTable);
        /// <summary>
        /// Exports list to JSON, default path is "entity.json"
        /// </summary>
        /// <param name="list"></param>
        /// <returns>JSON string</returns>
        string ExportToJSON(List<T> list);
        /// <summary>
        /// Exports list to JSON by your path
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="path"></param>
        /// <returns>JSON string</returns>
        string ExportToJSON(DataTable dataTable, string path);
        /// <summary>
        /// Exports list to JSON by your path
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        /// <returns>JSON string</returns>
        string ExportToJSON(List<T> list, string path);
        #endregion
        #region Exports to XML
        /// <summary>
        /// Exports datatable to XML, default path is "entity.xml"
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns>XML string</returns>
        string ExportToXML(DataTable dataTable);
        /// <summary>
        /// Exports list to XML, default path if "entity.xml"
        /// </summary>
        /// <param name="list"></param>
        /// <returns>XML string</returns>
        string ExportToXML(List<T> list);      
        /// <summary>
        /// Exports list to XML by your path
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="path"></param>
        /// <returns>XML string</returns>
        string ExportToXML(DataTable dataTable, string path);      
        /// <summary>
        /// Exports list to XML by your path
        /// </summary>
        /// <param name="list"></param>
        /// <param name="path"></param>
        /// <returns>XML string</returns>
        string ExportToXML(List<T> list, string path);
        #endregion
        #region Imports from JSON
        /// <summary>
        /// Imports list of entities into database from json file
        /// </summary>
        /// <param name="path">path where the json file is</param>
        void ImportFromJSON(string path);
        #endregion
        #region Imports from XML
        /// <summary>
        /// Imports list of entities into database from xml file
        /// </summary>
        /// <param name="path">path where the xml path is</param>
        void ImportFromXML(string path);
        #endregion
    }
}
