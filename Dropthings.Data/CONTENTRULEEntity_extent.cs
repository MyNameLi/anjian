using System;
using System.Collections.Generic;
using System.Data;

using System.Data.SqlClient;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace Dropthings.Data
{
    public partial class CONTENTRULEEntity
    {
        public string GetParserConfig()
        {
            switch (this.FIELDTYPE.Value)
            {
                case 1:
                    break;
                case 2:
                    return GetParserXPathXmlStr();
                case 3:
                    return LoadSEXmlStr();
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        private string GetParserXPathXmlStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<var-def name=\"{0}\" isfield=\"{1}\"><htmlxpath expression=\"{2}\" outputtype=\"{3}\" nohtml=\"{4}\"><var name=\"{5}\" /></htmlxpath></var-def>", this.FIELDSTR, 1 - this.ISINTERVAR.Value, this.FIELDREG, this.PARAM1, this.ISREMOVEHTML.Value, this.FIELDSOURCE));
            if (this.ISDATE.Value == 1)
            {
                sb.Append(string.Format("<importdate fromformat=\"{0}\" fieldname=\"MYDREDATE\" toformat=\"\" isfield=\"true\"><var name=\"MYPUBDATE\"/></importdate>", this.PARAM4));
                sb.Append(string.Format("<importdate fromformat=\"{0}\" fieldname=\"DREDATE\" toformat=\"long\" isfield=\"true\"><var name=\"MYPUBDATE\"/></importdate>", this.PARAM4));
            }
            if (this.FIELDSTR.Contains("DRECONTENT"))
            {
                sb.Append(string.Format("<var-def name=\"{0}\" isfield=\"{1}\"><htmlxpath expression=\"{2}\" outputtype=\"{3}\" nohtml=\"{4}\"><var name=\"{5}\" /></htmlxpath></var-def>", "DISPLAYCONTENT", 1 - this.ISINTERVAR.Value, this.FIELDREG, "nodehtml", 0, this.FIELDSOURCE));
            }
            return sb.ToString();
        }

        private string LoadSEXmlStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("<var-def name=\"{0}\" isfield=\"{1}\"><textstartend nohtml=\"{2}\"><start><![CDATA[{3}]]></start><end><![CDATA[{4}]]></end><source><var name=\"{5}\"/></source></textstartend></var-def>", this.FIELDSTR, 1 - this.ISINTERVAR.Value, this.ISREMOVEHTML.Value, this.FIELDREG, this.FIELDSUFFIX, this.FIELDSOURCE));
            if (this.ISDATE.Value == 1)
            {
                sb.Append(string.Format("<importdate fromformat=\"{0}\" fieldname=\"MYDREDATE\" toformat=\"\" isfield=\"true\"><var name=\"MYPUBDATE\"/></importdate>", this.PARAM4));
                sb.Append(string.Format("<importdate fromformat=\"{0}\" fieldname=\"DREDATE\" toformat=\"long\" isfield=\"true\"><var name=\"MYPUBDATE\"/></importdate>", this.PARAM4));
            }
            return sb.ToString();
        }
    }

}