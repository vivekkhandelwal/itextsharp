/*
    This file is part of the iText (R) project.
    Copyright (c) 1998-2019 iText Group NV
    Authors: iText Software.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    ITEXT GROUP. ITEXT GROUP DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
    http://itextpdf.com/terms-of-use/
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    In accordance with Section 7(b) of the GNU Affero General Public License,
    a covered work must retain the producer line in every PDF that is created
    or manipulated using iText.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the iText software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving PDFs on the fly in a web application, shipping iText with a closed
    source product.
    
    For more information, please contact iText Software Corp. at this
    address: sales@itextpdf.com
 */
using System;
using System.IO;
using System.Text;
using iTextSharp.testutils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NUnit.Framework;

namespace itextsharp.tests.iTextSharp.text.pdf {
    public class VerticalPositionTest : BaseTest {
        private const string TEST_RESOURCES_PATH = @"..\..\resources\text\pdf\VerticalPositionTest\";
        private const string OUTPUT_FOLDER = @"VerticalPositionTest\";

        [SetUp]
        public static void Init() {
            Directory.CreateDirectory(OUTPUT_FOLDER);
        }

        [Test]
        public void VerticalPositionTest0() {
            String file = "vertical_position.pdf";

            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, File.Create(OUTPUT_FOLDER + file));
            document.Open();

            writer.PageEvent = new CustomPageEvent();

            PdfPTable table = new PdfPTable(2);
            for (int i = 0; i < 100; i++) {
                table.AddCell("Hello " + i);
                table.AddCell("World " + i);
            }

            document.Add(table);

            document.NewPage();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 1000; i++) {
                sb.Append("some more text ");
            }
            document.Add(new Paragraph(sb.ToString()));

            document.Close();

            // compare
            CompareTool compareTool = new CompareTool();
            String errorMessage = compareTool.CompareByContent(OUTPUT_FOLDER + file, TEST_RESOURCES_PATH + file,
                OUTPUT_FOLDER, "diff");
            if (errorMessage != null) {
                Assert.Fail(errorMessage);
            }
        }
    }

    internal class CustomPageEvent : PdfPageEventHelper {
        public override void OnEndPage(PdfWriter writer, Document document) {
            Rectangle pageSize = writer.PageSize;
            float verticalPosition = writer.GetVerticalPosition(false);
            PdfContentByte canvas = writer.DirectContent;
            Rectangle rect = new Rectangle(0, verticalPosition, pageSize.Right, pageSize.Top);
            rect.Border = Rectangle.BOX;
            rect.BorderWidth = 1;
            rect.BorderColor = BaseColor.BLUE;
            canvas.Rectangle(rect);
        }
    }
}
