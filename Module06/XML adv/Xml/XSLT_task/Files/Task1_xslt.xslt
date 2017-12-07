<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/">
    <channel>
      <title>RSS</title>
      <link>Rss link</link>
      <description>Rss description</description>
      <xsl:for-each select="/catalog/book">
        <item>
          <title>
            Date: <xsl:value-of select="registration_date"/>
          </title>
          <link>
            <xsl:variable name ="id">
              <xsl:value-of select="@id"/>
            </xsl:variable>
            <xsl:value-of select="concat('my.safaribooksonline.com/', $id)" />
          </link>
          <description>
            <xsl:value-of select="description"/>
          </description>
        </item>
      </xsl:for-each>
    </channel> 
  </xsl:template>

</xsl:stylesheet>
