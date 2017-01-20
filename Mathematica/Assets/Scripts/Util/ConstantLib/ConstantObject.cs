using System;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Constant object for xml serialization
/// </summary>
[Serializable]
public class ConstantObject {
    [XmlArray("constants")]
    [XmlArrayItem("constant")]
    public ConstantItem[] constant_xml;

    public class ConstantItem {
        [XmlAttribute("key")]
        public string key;
        [XmlAttribute("value")]
        public string value;

        public ConstantItem () {
            key = "";
            value = "";
        }

        public ConstantItem (string key, string value) {
            this.key = key;
            this.value = value;
        }
    }

    public ConstantObject () {
        constant_xml = new ConstantItem[0];
    }
}
