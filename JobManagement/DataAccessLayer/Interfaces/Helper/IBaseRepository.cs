using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces.Helper
{
    public interface IBaseRepository<M> where M : class
    {
        // Gibt den Tabellennamen zurück, auf die sich das Repository bezieht
        string TableName { get; }

        // Liefert ein einzelnes Model-Objekt vom Typ M zurück, welches anhand dem übergebenen PrimaryKey geladen wird
        M GetSingleById<P>(P pkValue);

        // Liefert alle Model-Objekte vom Typ M zurück.
        List<M> GetAll();

        // Liefert Model-Objekte vom Typ M zurück, die dem Suchstring entsprechen.
        List<M> GetBySearchTerm(string searchTerm);

        // Fügt das Model-Objekt zur Datenbank hinzu (Insert)
        void Add(M entity);

        // Löscht das Model-Objekt aus der Datenbank
        string Delete(M entity);

        // Aktualisiert das Model-Objekt in der Datenbank
        void Update(M entity);
    }
}
