<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
                xmlns:excel="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:o="urn:schemas-microsoft-com:office:office"
 xmlns:x="urn:schemas-microsoft-com:office:excel"
 xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"
 xmlns:html="http://www.w3.org/TR/REC-html40"
xmlns:aml="http://schemas.microsoft.com/aml/2001/core" xmlns:wpc="http://schemas.microsoft.com/office/word/2010/wordprocessingCanvas" xmlns:dt="uuid:C2F41010-65B3-11d1-A29F-00AA00C14882" xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"  xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w10="urn:schemas-microsoft-com:office:word" xmlns:w="http://schemas.microsoft.com/office/word/2003/wordml" xmlns:wx="http://schemas.microsoft.com/office/word/2003/auxHint" xmlns:wne="http://schemas.microsoft.com/office/word/2006/wordml" xmlns:wsp="http://schemas.microsoft.com/office/word/2003/wordml/sp2" xmlns:sl="http://schemas.microsoft.com/schemaLibrary/2003/core">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="/">
      <xsl:processing-instruction name="mso-application">
        <xsl:value-of select="'progid=&quot;Excel.Sheet&quot;'"/>
      </xsl:processing-instruction>
      <Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet" xmlns:html="http://www.w3.org/TR/REC-html40" xmlns:x2="http://schemas.microsoft.com/office/excel/2003/xml">
        <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">
          <Author>OMSG</Author>
          <LastAuthor>OMSG</LastAuthor>
          <Created></Created>
          <LastSaved></LastSaved>
          <Version>1.00</Version>
        </DocumentProperties>
        <OfficeDocumentSettings xmlns="urn:schemas-microsoft-com:office:office">
          <AllowPNG/>
        </OfficeDocumentSettings>
        <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">
          <WindowHeight>7965</WindowHeight>
          <WindowWidth>17955</WindowWidth>
          <WindowTopX>720</WindowTopX>
          <WindowTopY>315</WindowTopY>
          <ProtectStructure>False</ProtectStructure>
          <ProtectWindows>False</ProtectWindows>
          <FutureVer>11</FutureVer>
        </ExcelWorkbook>
        <Styles>
          <Style ss:ID="Default" ss:Name="Normal">
            <Alignment ss:Vertical="Bottom"/>
            <Borders/>
            <Font ss:FontName="Calibri" x:Family="Swiss" ss:Size="11" ss:Color="#000000"/>
            <Interior/>
            <NumberFormat/>
            <Protection/>
          </Style>
          <Style ss:ID="s62">
            <NumberFormat ss:Format="@"/>
          </Style>
          <Style ss:ID="s63">
            <NumberFormat ss:Format="General Date"/>
          </Style>
          <Style ss:ID="s64">
            <NumberFormat ss:Format="0"/>
          </Style>
          <Style ss:ID="sHeader">
            <Interior ss:Color="#538DD5" ss:Pattern="Solid"/>
          </Style>
          <Style ss:ID="sRowStringOdd">
            <Interior ss:Color="#C5D9F1" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="@"/>
          </Style>
          <Style ss:ID="sRowStringEven">
            <Interior ss:Color="#8DB4E2" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="@"/>
          </Style>
          <Style ss:ID="sRowNumberOdd">
            <Interior ss:Color="#C5D9F1" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="0"/>
          </Style>
          <Style ss:ID="sRowNumberEven">
            <Interior ss:Color="#8DB4E2" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="0"/>
          </Style>
          <Style ss:ID="sRowDateOdd">
            <Interior ss:Color="#C5D9F1" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="General Date"/>
          </Style>
          <Style ss:ID="sRowDateEven">
            <Interior ss:Color="#8DB4E2" ss:Pattern="Solid"/>
            <NumberFormat ss:Format="General Date"/>
          </Style>
        </Styles>
        <Worksheet ss:Name="DPKID-SN Bundles">
          <Table ss:ExpandedColumnCount="12" ss:ExpandedRowCount="161" x:FullColumns="1" x:FullRows="1" ss:DefaultRowHeight="15">
            <Column ss:Width="417"/>
            <Column ss:Width="162"/>
            <Column ss:Width="426"/>
            <Column ss:Width="140.25"/>
            <Column ss:Width="192.75"/>
            <Column ss:Width="213.75"/>
            <Column ss:Width="417"/>
            <Column ss:Width="426"/>
            <Column ss:Width="172.5"/>
            <Column ss:Width="193.5"/>
            <Column ss:Width="417"/>
            <Column ss:Width="426"/>
            <Row>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Transaction ID</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Serial Number</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Product Key ID</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Series Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Model Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Device ID</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Order ID</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Business Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Line Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Station Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Operator Name</Data>
              </Cell>
              <Cell ss:StyleID="sHeader">
                <Data ss:Type="String">Creation Time</Data>
              </Cell>
            </Row>
            <xsl:for-each select="//ProductKeyIDSerialNumberPairs">
              <Row>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="TransactionID"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="SerialNumber"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowNumberEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowNumberOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="ProductKeyID"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="SeriesName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowNumberEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowNumberOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="ModelName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="DeviceID"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowNumberEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowNumberOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="OrderID"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="BusinessName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="LineName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="StationName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowNumberEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowNumberOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="OperatorName"/>
                  </Data>
                </Cell>
                <Cell>
                  <xsl:attribute name="ss:StyleID">
                    <xsl:if test="position() mod 2 = 0">
                      <xsl:value-of select="'sRowStringEven'"/>
                    </xsl:if>
                    <xsl:if test="position() mod 2 != 0">
                      <xsl:value-of select="'sRowStringOdd'"/>
                    </xsl:if>
                  </xsl:attribute>
                  <Data ss:Type="String">
                    <xsl:value-of select="CreationTime"/>
                  </Data>
                </Cell>
              </Row>
            </xsl:for-each>
          </Table>
          <WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">
            <PageSetup>
              <Header x:Margin="0.3"/>
              <Footer x:Margin="0.3"/>
              <PageMargins x:Bottom="0.75" x:Left="0.7" x:Right="0.7" x:Top="0.75"/>
            </PageSetup>
            <Print>
              <ValidPrinterInfo/>
              <PaperSizeIndex>9</PaperSizeIndex>
              <HorizontalResolution>600</HorizontalResolution>
              <VerticalResolution>600</VerticalResolution>
            </Print>
            <Selected/>
            <Panes>
              <Pane>
                <Number>3</Number>
                <ActiveRow>21</ActiveRow>
              </Pane>
            </Panes>
            <ProtectObjects>False</ProtectObjects>
            <ProtectScenarios>False</ProtectScenarios>
          </WorksheetOptions>
        </Worksheet>
      </Workbook>
    </xsl:template>
</xsl:stylesheet>
