﻿using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    public class AverageNetSalarySeeder : BaseSeeder
    {
        public static List<Data> GetData()
        {
            var rawData = new List<string>
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 1437 1430 1555 1476 1525 1538 1535 1526 1526 1550 1535 1560 1610 1638 1816",
                                  "2014 10 Arad 1540 1593 1645 1545 1553 1602 1588 1559 1623 1682 1589 1650 1676 1728 1855",
                                  "2014 10 Arges 1675 1697 1900 1627 1639 1695 1833 1794 1975 1923 1728 1738 1753 1740 1991",
                                  "2014 10 Bacau 1367 1407 1511 1299 1291 1380 1381 1375 1403 1436 1423 1432 1459 1509 1661",
                                  "2014 10 Bihor 1271 1296 1327 1276 1261 1314 1319 1313 1336 1363 1342 1369 1406 1439 1515",
                                  "2014 10 BistritaNasaud 1317 1349 1353 1271 1256 1305 1306 1279 1300 1366 1347 1399 1429 1441 1486",
                                  "2014 10 Botosani 1225 1235 1307 1316 1295 1316 1328 1304 1322 1355 1336 1354 1388 1405 1564",
                                  "2014 10 Brasov 1608 1698 1756 1658 1661 1795 1753 1718 1789 1761 1734 1749 1808 1891 2040",
                                  "2014 10 Braila 1268 1263 1342 1300 1282 1310 1343 1296 1305 1362 1322 1334 1372 1377 1515",
                                  "2014 10 Buzau 1355 1357 1502 1355 1358 1408 1420 1404 1415 1427 1433 1446 1473 1473 1626",
                                  "2014 10 CarasSeverin 1262 1293 1277 1276 1281 1307 1328 1331 1331 1351 1358 1379 1415 1415 1459",
                                  "2014 10 Calarasi 1317 1320 1379 1345 1334 1410 1380 1398 1407 1481 1418 1453 1505 1510 1627",
                                  "2014 10 Cluj 1819 1869 1922 1847 1838 1925 2010 1916 1990 2036 1961 1987 2060 2104 2358",
                                  "2014 10 Constanta 1572 1615 1745 1669 1659 1729 1805 1758 1729 1773 1760 1788 1840 1877 2111",
                                  "2014 10 Covasna 1240 1240 1316 1273 1252 1329 1268 1276 1289 1318 1319 1317 1353 1368 1502",
                                  "2014 10 Dambovita 1355 1359 1430 1422 1390 1457 1499 1435 1494 1506 1479 1511 1544 1537 1682",
                                  "2014 10 Dolj 1505 1549 1684 1629 1542 1546 1605 1547 1637 1630 1588 1586 1673 1655 1821",
                                  "2014 10 Galati 1563 1562 1709 1604 1593 1637 1679 1618 1637 1669 1670 1662 1687 1697 1909",
                                  "2014 10 Giurgiu 1221 1259 1336 1345 1308 1339 1403 1382 1358 1407 1375 1407 1471 1464 1629",
                                  "2014 10 Gorj 1619 1667 1767 1660 1649 1728 1902 1702 1655 1770 1807 1745 1799 1830 2100",
                                  "2014 10 Harghita 1132 1127 1164 1187 1191 1219 1224 1213 1242 1275 1256 1284 1317 1316 1419",
                                  "2014 10 Hunedoara 1419 1418 1521 1446 1442 1490 1522 1491 1451 1503 1467 1471 1524 1551 1684",
                                  "2014 10 Ialomita 1309 1303 1396 1287 1287 1338 1344 1343 1343 1351 1351 1399 1423 1433 1570",
                                  "2014 10 Iasi 1562 1595 1657 1624 1613 1683 1674 1692 1676 1700 1705 1721 1767 1831 1950",
                                  "2014 10 Ilfov 1955 2107 2227 2009 2018 2174 2083 2088 2080 2160 2092 2135 2152 2215 2504",
                                  "2014 10 Maramures 1238 1271 1285 1255 1256 1302 1289 1287 1305 1369 1346 1369 1418 1472 1533",
                                  "2014 10 Mehedinti 1411 1454 1560 1426 1412 1466 1491 1465 1446 1481 1562 1553 1510 1520 1675",
                                  "2014 10 Mures 1484 1488 1634 1548 1584 1641 1804 1616 1644 1652 1647 1641 1683 1701 1914",
                                  "2014 10 Neamt 1250 1235 1311 1267 1258 1298 1307 1286 1272 1301 1323 1330 1361 1362 1497",
                                  "2014 10 Olt 1419 1444 1547 1444 1445 1562 1510 1496 1601 1564 1622 1583 1595 1662 1820",
                                  "2014 10 Prahova 1609 1646 1722 1601 1617 1694 1682 1670 1639 1658 1646 1680 1695 1746 1876",
                                  "2014 10 SatuMare 1357 1350 1387 1317 1321 1439 1365 1370 1379 1421 1404 1423 1475 1475 1573",
                                  "2014 10 Salaj 1486 1352 1397 1320 1346 1359 1346 1330 1368 1440 1396 1447 1565 1456 1562",
                                  "2014 10 Sibiu 1750 1785 2101 1841 1784 1884 2004 1941 1865 1939 1885 1906 1954 2040 2188",
                                  "2014 10 Suceava 1242 1241 1312 1246 1190 1232 1234 1264 1240 1261 1264 1299 1315 1327 1462",
                                  "2014 10 Teleorman 1207 1227 1243 1368 1340 1360 1376 1368 1379 1410 1421 1418 1460 1482 1577",
                                  "2014 10 Timis 1773 1988 1985 1799 1815 1957 1961 1887 1966 1944 1870 1910 1966 2201 2218",
                                  "2014 10 Tulcea 1424 1398 1503 1350 1338 1374 1514 1374 1405 1440 1440 1393 1424 1432 1589",
                                  "2014 10 Vaslui 1237 1231 1274 1228 1218 1272 1288 1265 1255 1309 1282 1301 1365 1371 1480",
                                  "2014 10 Valcea 1341 1366 1438 1378 1376 1415 1472 1457 1452 1501 1506 1497 1530 1585 1664",
                                  "2014 10 Vrancea 1254 1241 1326 1290 1279 1328 1336 1321 1327 1369 1388 1372 1438 1438 1622",
                                  "2014 10 Bucuresti 2361 2389 2623 2419 2405 2594 2637 2540 2524 2554 2504 2533 2556 2617 2958",
                                    // 2016
"2016 1 Alba 1689 1737 1795 1792 1800 1827 1832 1843 1872 1903 1950 2121",
"2016 1 Arad 1705 1729 1781 1809 1846 1903 1883 1852 1910 1936 1990 2118",
"2016 1 Arges 1772 1792 1801 2231 1988 1903 2036 1918 1927 1932 1927 2160",
"2016 1 Bacau 1611 1619 1697 1685 1701 1758 1760 1803 1850 1796 1861 2083",
"2016 1 Bihor 1486 1464 1508 1518 1583 1590 1601 1611 1628 1645 1684 1753",
"2016 1 BistritaNasaud 1484 1454 1520 1504 1587 1551 1609 1570 1548 1562 1630 1653",
"2016 1 Botosani 1502 1497 1530 1556 1557 1554 1542 1578 1588 1623 1626 1763",
"2016 1 Brasov 1827 1831 1923 1984 1939 2030 1955 1968 1998 2031 2143 2284",
"2016 1 Braila 1421 1422 1467 1486 1528 1554 1550 1544 1548 1582 1639 1795",
"2016 1 Buzau 1588 1588 1610 1641 1698 1700 1704 1743 1718 1726 1752 1873",
"2016 1 CarasSeverin 1430 1453 1475 1515 1546 1580 1545 1586 1653 1681 1733 1744",
"2016 1 Calarasi 1559 1562 1613 1648 1669 1719 1701 1690 1747 1768 1786 1850",
"2016 1 Cluj 2156 2131 2254 2269 2316 2284 2372 2370 2349 2366 2469 2605",
"2016 1 Constanta 1804 1784 1845 1930 1871 1886 1909 1904 1914 1942 1969 2184",
"2016 1 Covasna 1420 1423 1530 1449 1516 1543 1536 1623 1566 1584 1599 1788",
"2016 1 Dambovita 1614 1571 1674 1627 1672 1678 1696 1730 1749 1764 1748 1838",
"2016 1 Dolj 1726 1761 1738 1773 1786 1788 1836 1822 1821 1868 1896 2020",
"2016 1 Galati 1746 1747 1798 1849 1864 1866 1862 1918 1913 1925 1953 2154",
"2016 1 Giurgiu 1607 1594 1629 1625 1708 1661 1636 1660 1685 1705 1718 1926",
"2016 1 Gorj 1860 1812 1819 1923 1845 1861 1924 1967 1891 1979 2011 2090",
"2016 1 Harghita 1373 1355 1402 1387 1473 1480 1496 1523 1540 1532 1558 1671",
"2016 1 Hunedoara 1569 1578 1593 1627 1605 1620 1608 1636 1680 1676 1711 1823",
"2016 1 Ialomita 1448 1449 1494 1542 1539 1570 1519 1550 1582 1590 1662 1711",
"2016 1 Iasi 1851 1843 1888 1907 1924 1946 1926 1927 1968 2015 2020 2183",
"2016 1 Ilfov 2182 2174 2316 2309 2307 2294 2362 2323 2333 2368 2456 2743",
"2016 1 Maramures 1451 1455 1488 1494 1520 1541 1543 1590 1603 1622 1697 1765",
"2016 1 Mehedinti 1629 1636 1658 1673 1707 1696 1668 1718 1713 1733 1753 1881",
"2016 1 Mures 1708 1702 1758 1901 1836 1880 1815 1838 1846 1854 1875 2156",
"2016 1 Neamt 1388 1416 1437 1468 1508 1546 1532 1526 1550 1569 1582 1705",
"2016 1 Olt 1610 1591 1650 1722 1701 1735 1701 1772 1737 1735 1856 1977",
"2016 1 Prahova 1781 1815 1919 1897 1913 1906 1892 1934 1947 1928 1981 2148",
"2016 1 SatuMare 1495 1538 1601 1535 1579 1569 1565 1587 1599 1651 1659 1741",
"2016 1 Salaj 1470 1488 1511 1544 1557 1566 1592 1596 1591 1728 1687 1744",
"2016 1 Sibiu 1997 1963 2072 2216 2174 2104 2132 2135 2139 2116 2227 2375",
"2016 1 Suceava 1392 1390 1436 1470 1557 1517 1488 1510 1533 1561 1547 1672",
"2016 1 Teleorman 1439 1427 1469 1457 1475 1494 1497 1530 1506 1555 1561 1628",
"2016 1 Timis 2001 2019 2142 2221 2163 2240 2211 2162 2191 2231 2507 2512",
"2016 1 Tulcea 1553 1537 1596 1662 1670 1670 1696 1724 1677 1718 1755 1926",
"2016 1 Vaslui 1472 1460 1491 1492 1544 1533 1550 1552 1636 1625 1637 1728",
"2016 1 Valcea 1512 1490 1528 1573 1568 1592 1579 1613 1628 1646 1655 1768",
"2016 1 Vrancea 1438 1436 1460 1444 1557 1571 1562 1587 1602 1584 1580 1707",
"2016 1 Bucuresti 2670 2702 2931 2917 2824 2852 2826 2800 2832 2818 2896 3219",

                              };

            var items = GetItems("AverageNetSalary", rawData);

            return items;
        }
    }
}
