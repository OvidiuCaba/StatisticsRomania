﻿using StatisticsRomania.BusinessObjects;
using System.Collections.Generic;

namespace StatisticsRomania.Repository.Seeders
{
    internal class NumberOfNightsSeeder : BaseSeeder
    {
        internal static List<NumberOfNights> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 17302 14510 12761 12737 15030 16021 19951 29400 30065 40081 45626 30680 25193 25843 21384",
                                  "2014 10 Arad 34800 30121 22964 18935 19790 24862 24910 32221 34724 46098 49831 39184 33520 30803 23109",
                                  "2014 10 Arges 21842 19739 21550 17373 15750 16343 20759 23628 29437 42951 47000 36757 27638 24570 23567",
                                  "2014 10 Bacau 24053 19422 17187 16594 16149 17939 17676 23509 34977 39471 46873 35542 29934 22476 19786",
                                  "2014 10 Bihor 104051 94330 66401 31169 40097 42806 52261 84621 121405 158799 179431 135865 117816 105275 63451",
                                  "2014 10 BistritaNasaud 12874 7922 7784 7566 7668 10042 9129 16217 18880 24235 32564 27010 19813 14386 8059",
                                  "2014 10 Botosani 6074 5224 4065 3668 3162 5267 4439 6190 7814 6604 6020 6046 7499 5386 5960",
                                  "2014 10 Brasov 140114 137242 173499 182211 171308 122848 128402 148877 163047 202079 257674 185080 160166 151105 175894",
                                  "2014 10 Braila 21765 20343 11292 6801 8878 10927 11624 19590 23191 29418 29675 24702 23393 19575 10039",
                                  "2014 10 Buzau 19521 15995 8929 7517 8883 10601 10832 15254 15820 19264 21147 18113 16015 15009 10284",
                                  "2014 10 CarasSeverin 49266 39202 22036 18161 21924 24637 30962 65016 81723 103766 116765 86140 71155 59594 29260",
                                  "2014 10 Calarasi 3038 2316 2866 1739 2276 2766 2188 3177 2807 3716 4230 3553 4027 3046 1996",
                                  "2014 10 Cluj 66193 59612 40332 39540 43767 59689 70488 81135 88193 87619 84044 82532 90877 77897 49571",
                                  "2014 10 Constanta 80475 57997 38533 31625 33170 45350 62895 149203 455616 1315634 1522826 347427 94592 74176 48792",
                                  "2014 10 Covasna 63857 51025 17394 10787 14394 22518 22997 44184 58350 69755 66204 57317 44926 48170 21244",
                                  "2014 10 Dambovita 23983 22826 14634 9711 12060 16656 15724 21624 24334 27974 29320 26418 24385 23801 16220",
                                  "2014 10 Dolj 14830 14875 13211 11204 13805 14466 14366 16294 17407 17262 16433 17307 17228 16153 15159",
                                  "2014 10 Galati 11093 10332 7430 7296 8830 9042 12085 11791 13456 12315 12757 12387 13237 12619 8808",
                                  "2014 10 Giurgiu 2888 3708 2701 2493 2329 3055 4016 3989 4099 4817 4026 3353 3439 3262 2049",
                                  "2014 10 Gorj 16673 10449 10714 10419 9058 7964 8814 12193 11760 21259 20951 13382 9947 9651 11414",
                                  "2014 10 Harghita 28047 21559 20371 17476 19531 12712 26189 36008 34745 57167 68417 40600 34119 28702 28947",
                                  "2014 10 Hunedoara 24250 24438 14252 11918 15192 20819 23261 29441 34892 47445 50647 36770 27794 26201 19272",
                                  "2014 10 Ialomita 19103 4546 4051 6811 11390 12955 6629 18740 28012 37112 29960 24508 17431 11015 6412",
                                  "2014 10 Iasi 43392 36244 23827 20400 24964 33200 33683 43939 42593 44561 39800 46965 49745 43948 30363",
                                  "2014 10 Ilfov 15401 12138 11304 11291 10669 13237 13201 16035 16323 16470 14361 17386 16463 14286 11771",
                                  "2014 10 Maramures 16813 13910 21424 15517 7423 14412 17585 245350 25891 34562 32413 27838 23631 18751 27874",
                                  "2014 10 Mehedinti 9580 8545 4915 3692 6005 7542 9786 13102 17479 24103 28174 19730 15253 12068 8653",
                                  "2014 10 Mures 74255 61422 48121 51183 54460 54188 61598 89485 92444 129774 145034 104802 96159 80961 59584",
                                  "2014 10 Neamt 26150 16973 17559 12701 11217 11664 19619 30375 38207 51695 56581 37956 25602 16029 18162",
                                  "2014 10 Olt 6615 5660 4174 4483 5409 7499 6923 7823 6735 5225 5385 5930 6220 6119 5616",
                                  "2014 10 Prahova 86234 68836 76625 65357 74270 59481 61977 78044 79361 109480 130074 98205 89955 84473 85437",
                                  "2014 10 SatuMare 14787 13339 12134 12158 12378 12598 11743 12509 14018 15665 15429 13730 11866 11640 11994",
                                  "2014 10 Salaj 7314 6837 4381 3709 5571 6610 5862 7177 7384 10380 11195 8140 7892 7984 5279",
                                  "2014 10 Sibiu 42723 41122 39346 27856 31113 34881 38517 59379 67170 103522 120176 85835 49067 60794 58935",
                                  "2014 10 Suceava 47454 37769 48672 42231 41496 34152 49169 62768 68332 85926 94243 66309 52590 46060 51041",
                                  "2014 10 Teleorman 1899 1935 1134 1170 1311 1364 1217 2033 3571 2478 2520 1848 2037 1789 1551",
                                  "2014 10 Timis 72925 61393 42020 36586 42120 53967 59998 72755 81179 80182 83853 86191 82545 73090 48937",
                                  "2014 10 Tulcea 5023 4641 2329 2183 2937 4048 6950 13977 22319 25345 29925 17358 7097 3237 2801",
                                  "2014 10 Vaslui 4888 3563 2798 2866 2447 5245 5203 4948 4573 6697 5432 5717 5519 4790 4501",
                                  "2014 10 Valcea 86184 85734 47350 26515 33223 34690 67439 108465 124888 170289 182404 129365 116211 100949 57536",
                                  "2014 10 Vrancea 5242 6085 4745 3814 3721 3643 5295 4665 6223 7531 7162 5949 6202 6316 15062",
                                  "2014 10 Bucuresti 256645 226101 178290 161088 175600 234859 227787 273978 277372 267677 239075 273701 281306 248272 207376",
                                  // 2016
                                    "2016 1 Alba 18065 17659 17234 19249 24768 28866 38871",
                                    "2016 1 Arad 18060 22017 26715 28914 37326 34750 42549",
                                    "2016 1 Arges 18571 20203 20028 19653 25145 31392 44979",
                                    "2016 1 Bacau 15525 17265 20357 25424 32878 31529 39374",
                                    "2016 1 Bihor 55043 57778 58479 80340 120268 143813 185407",
                                    "2016 1 BistritaNasaud 6818 8160 9249 9663 16095 20078 25547",
                                    "2016 1 Botosani 4347 4373 5458 5690 6281 7015 8240",
                                    "2016 1 Brasov 181397 169349 114548 129211 146006 170313 236444",
                                    "2016 1 Braila 6072 11014 11630 15783 20152 22730 28065",
                                    "2016 1 Buzau 7706 8195 10780 10013 13690 14945 20522",
                                    "2016 1 CarasSeverin 27437 26232 22152 40591 62942 72079 98142",
                                    "2016 1 Calarasi 1953 1813 2109 2844 2529 3154 3956",
                                    "2016 1 Cluj 47236 60184 66897 76162 87722 91924 106070",
                                    "2016 1 Constanta 39920 49446 48705 64277 122525 462149 1455481",
                                    "2016 1 Covasna 9968 13420 19471 33198 50253 54457 70380",
                                    "2016 1 Dambovita 11828 15048 17687 18672 17511 23938 28383",
                                    "2016 1 Dolj 11918 12940 14993 14765 14581 15867 14351",
                                    "2016 1 Galati 8344 8998 11480 10779 13687 15384 11920",
                                    "2016 1 Giurgiu 2395 3054 4381 3596 4449 4456 5252",
                                    "2016 1 Gorj 10705 10234 7050 8029 9808 10785 22507",
                                    "2016 1 Harghita 27934 28557 18650 23667 45469 46631 59119",
                                    "2016 1 Hunedoara 13241 19746 21000 22042 26801 30206 39684",
                                    "2016 1 Ialomita 2176 3615 3214 9740 15811 22597 32507",
                                    "2016 1 Iasi 27774 32962 40251 44338 51767 48978 52999",
                                    "2016 1 Ilfov 12019 13083 15188 16160 19872 19706 20138",
                                    "2016 1 Maramures 18678 20053 19018 23374 28010 31954 40322",
                                    "2016 1 Mehedinti 8252 9011 12008 12827 15600 18504 27339",
                                    "2016 1 Mures 57344 64077 57300 76192 86942 88793 131481",
                                    "2016 1 Neamt 14238 14021 17163 25910 32157 47636 69266",
                                    "2016 1 Olt 4598 5766 6514 6090 5748 6495 6042",
                                    "2016 1 Prahova 79802 79909 58127 69814 86260 94508 115139",
                                    "2016 1 SatuMare 11079 10573 11084 10903 10928 11209 11680",
                                    "2016 1 Salaj 3098 4915 6314 6696 7390 7261 8551",
                                    "2016 1 Sibiu 42030 41906 38763 55642 65631 76028 105384",
                                    "2016 1 Suceava 37606 35435 29748 38042 64191 71936 94148",
                                    "2016 1 Teleorman 880 1025 1404 1561 1662 1425 1963",
                                    "2016 1 Timis 50882 58162 74121 67627 83460 78822 84882",
                                    "2016 1 Tulcea 1684 2428 2711 4677 13946 24381 24542",
                                    "2016 1 Vaslui 2477 4001 4220 4638 4808 4755 5837",
                                    "2016 1 Valcea 36123 47500 40970 72906 103659 130306 177425",
                                    "2016 1 Vrancea 4404 4385 5811 5692 6620 6676 10642",
                                    "2016 1 Bucuresti 185587 209212 243526 256148 302614 284761 272044",
                              };

            var items = GetItems<NumberOfNights>(rawData);

            return items;
        }
    }
}