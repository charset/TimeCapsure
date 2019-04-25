using System;
using System.Collections.Generic;
using System.Text;

namespace TimeCapsure.Validator {
    public abstract class AValidator {
        public abstract bool Validate(string message);
        public string Schema { get; protected set; }
        public string Name { get; protected set; }
    }
}
