<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:variable name ="total">
      <xsl:value-of select="count(/catalog/book)"/>
  </xsl:variable>

  <xsl:template match="/">
    <html>
      <body>
        <h1>Fond</h1>
        <table border="1">
          <tr>
            <th>Author</th>
            <th>Title</th>
            <th>Publish date</th>
            <th>Registration date</th>
          </tr>
          <xsl:for-each select="catalog/book">
            <tr>
              <td>
                <xsl:value-of select="author"/>
              </td>
              <td>
                <xsl:value-of select="title"/>
              </td>
              <td>
                <xsl:value-of select="publish_date"/>
              </td>
              <td>
                <xsl:value-of select="registration_date"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
        <h2>total books: <xsl:value-of select="$total"/>
      </h2>
      </body>
    </html>
  </xsl:template>
  
</xsl:stylesheet>
