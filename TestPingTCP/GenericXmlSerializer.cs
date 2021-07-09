using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace TestPingTCP
{
    class GenericXmlSerializer
    {
        public T Xml<T>(string xml, out string Error) where T : new()
        {
            T comprobante = new T();

            Error = "";
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                StringReader sXML = new StringReader(xml);
                comprobante = (T)serializer.Deserialize(sXML);

            }
            catch (Exception ex)
            {
                Error = "Error en la Serialización" + ex.Message;
                comprobante = new T();
            }
            return comprobante;

        }
    }
}
