using System.Xml.Linq;

namespace Dal;

///////////////////////////////////////
//implement IStudent with linq to XML
///////////////////////////////////////
class DLStudent : DLApi.IStudent
{
    const string s_students = "students"; //Linq to XML

    static DO.Student? createStudentfromXElement(XElement s)
    {
        return new DO.Student()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"), //fix to: DalXmlFormatException(id)),
            FirstName = (string?)s.Element("FirstName"),
            LastName = (string?)s.Element("LastName"),
            StudentStatus = s.ToEnumNullable<DO.StudentStatus>("StudentStatus"),
            BirthDate = s.ToDateTimeNullable("BirthDate"),
            Grade = (double?)s.Element("Grade") // s.ToDoubleNullable("Grade")
        };
    }
    public IEnumerable<DO.Student?> GetAll(Func<DO.Student?, bool>? filter = null)
    {
        XElement? studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        //return studentsRootElem.Elements().Select(s => createStudentfromXElement(s)).Where(filter);

        if (filter != null)
        {
            return from s in studentsRootElem.Elements()
                   let doStud = createStudentfromXElement(s)
                   where filter(doStud)
                   select (DO.Student?)doStud;
        }
        else
        {
            return from s in studentsRootElem.Elements()
                   select createStudentfromXElement(s);
        }

    }

    public DO.Student GetById(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        return (from s in studentsRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Student?)createStudentfromXElement(s)).FirstOrDefault()
                ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);
    }
    public int Add(DO.Student doStudent)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        XElement? stud = (from st in studentsRootElem.Elements()
                          where st.ToIntNullable("ID") == doStudent.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (stud != null)
            throw new Exception("id already exist"); // fix to: throw new DalMissingIdException(id);

        XElement studentElem = new XElement("Student",
                                   new XElement("ID", doStudent.ID),
                                   new XElement("FirstName", doStudent.FirstName),
                                   new XElement("LastName", doStudent.LastName),
                                   new XElement("StudentStatus", doStudent.StudentStatus),
                                   new XElement("BirthDate", doStudent.BirthDate),
                                   new XElement("Grade", doStudent.Grade)
                                   );

        studentsRootElem.Add(studentElem);

        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);

        return doStudent.ID; ;
    }

    public void Delete(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        XElement? stud = (from st in studentsRootElem.Elements()
                          where (int?)st.Element("ID") == id
                          select st).FirstOrDefault() ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);

        stud.Remove(); //<==>   Remove stud from studentsRootElem

        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);
    }

    public void Update(DO.Student doStudent)
    {
        Delete(doStudent.ID);
        Add(doStudent);
    }
}
