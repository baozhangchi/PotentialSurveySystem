#region

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#endregion

namespace DynamicSql
{
    [XmlRoot("sqlMap")]
    public class SqlMap
    {
        [XmlArray("alias")]
        [XmlArrayItem("alias")]
        public List<TypeAlias> Alias { get; set; } = new List<TypeAlias>();

        [XmlArray("resultMaps")]
        [XmlArrayItem("resultMap")]
        public List<ResultMap> ResultMaps { get; set; } = new List<ResultMap>();

        [XmlElement("statements")] public Statements Statements { get; set; } = new Statements();
    }

    public class Statements
    {
        [XmlElement("select")]
        public List<SelectStatement> SelectStatements { get; set; } = new List<SelectStatement>();

        [XmlElement("insert")]
        public List<InsertStatement> InsertStatements { get; set; } = new List<InsertStatement>();

        [XmlElement("update")]
        public List<UpdateStatement> UpdateStatements { get; set; } = new List<UpdateStatement>();

        [XmlElement("delete")]
        public List<DeleteStatement> DeleteStatements { get; set; } = new List<DeleteStatement>();

        [XmlIgnore]
        public List<Statement> StatementItems => SelectStatements
            .Union(InsertStatements.Cast<Statement>()).Union(UpdateStatements)
            .Union(DeleteStatements).ToList();

        public Statement this[string id]
        {
            get { return StatementItems.FirstOrDefault(x => x.Id == id); }
        }
    }

    public abstract class Statement
    {
        [XmlAttribute("id")] public string Id { get; set; }

        [XmlAttribute("resultMap")] public string ResultMap { get; set; }

        [XmlAttribute("parameterClass")] public string ParameterClass { get; set; }

        [XmlAttribute("extends")] public string Extends { get; set; }

        [XmlElement("selectKey")] public List<SelectKey> SelectKeys { get; set; } = new List<SelectKey>();

        [XmlText] public string Content { get; set; }
    }

    public class SelectKey
    {
        [XmlAttribute("property")] public string Property { get; set; }

        [XmlAttribute("type")] public string Type { get; set; }

        [XmlAttribute("resultClass")] public string ResultClass { get; set; }

        [XmlText] public string Content { get; set; }
    }

    public class DeleteStatement : Statement
    {
    }

    public class UpdateStatement : Statement
    {
    }

    public class InsertStatement : Statement
    {
    }

    public class SelectStatement : Statement
    {
    }

    public class ResultMap
    {
        [XmlAttribute("id")] public string Id { get; set; }

        [XmlAttribute("class")] public string Class { get; set; }


        [XmlElement("result")] public List<ResultItem> ResultItems { get; set; } = new List<ResultItem>();
    }

    public class ResultItem
    {
        [XmlAttribute("property")] public string Property { get; set; }

        [XmlAttribute("column")] public string Column { get; set; }
    }

    public class TypeAlias
    {
        [XmlAttribute("alias")] public string Alias { get; set; }

        [XmlAttribute("type")] public string Type { get; set; }
    }
}