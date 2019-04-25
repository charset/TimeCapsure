using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace TimeCapsure.Validator {
    public class XmlValidator : AValidator {
        private readonly XmlSchemaSet schema;

        public XmlValidator(string name, string schemaPath) {
            using(StreamReader sr = new StreamReader(schemaPath, Encoding.UTF8)) {
                string content = sr.ReadToEnd();
                schema = new XmlSchemaSet();
                schema.Add("", XmlReader.Create(new StringReader(content)));
                Name = name;
            }
        }

        public override bool Validate(string message) {
            XDocument xDoc = XDocument.Parse(message);
            bool validate = true;
            xDoc.Validate(schema, (o, e) => { validate = false; });
            return validate;
        }
    }

    public class JsonValidator : AValidator {
        private readonly JSchema schema;

        public JsonValidator(string name, string schemaPath) {
            using(StreamReader sr = new StreamReader(schemaPath, Encoding.UTF8)) {
                string content = sr.ReadToEnd();
                schema = JSchema.Parse(content);
                Schema = schemaPath;
                Name = name;
            }
        }

        public override bool Validate(string message) {
            JObject @object = JObject.Parse(message);
            return @object.IsValid(schema);
        }
    }

    public class ValidatorHelper {
        private readonly Dictionary<string, AValidator> validators = new Dictionary<string, AValidator>();

    }
}
