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
"2016 1 Alba 18065 17659 17234 19249 24768 28866 38871 45868 30365 23011 21411 21357",
"2016 1 Arad 18060 22017 26715 28914 37326 34750 42549 44335 38173 33597 32633 20918",
"2016 1 Arges 18571 20203 20028 19653 25145 31392 44979 53836 35058 30112 22742 22205",
"2016 1 Bacau 15525 17265 20357 25424 32878 31529 39374 43359 31721 27749 27079 19440",
"2016 1 Bihor 55043 57778 58479 80340 120268 143813 185407 210698 166701 132077 105903 69979",
"2016 1 BistritaNasaud 6818 8160 9249 9663 16095 20078 25547 32222 20425 14184 9118 6819",
"2016 1 Botosani 4347 4373 5458 5690 6281 7015 8240 7762 8026 6731 6460 7407",
"2016 1 Brasov 181397 169349 114548 129211 146006 170313 236444 293785 202795 170678 149406 221705",
"2016 1 Braila 6072 11014 11630 15783 20152 22730 28065 30481 25324 22835 21177 9919",
"2016 1 Buzau 7706 8195 10780 10013 13690 14945 20522 25023 19523 20069 14757 9818",
"2016 1 CarasSeverin 27437 26232 22152 40591 62942 72079 98142 109366 82177 59535 53186 31236",
"2016 1 Calarasi 1953 1813 2109 2844 2529 3154 3956 5837 3266 3570 3186 2157",
"2016 1 Cluj 47236 60184 66897 76162 87722 91924 106070 107231 88376 88492 76579 57162",
"2016 1 Constanta 39920 49446 48705 64277 122525 462149 1455481 1668705 440934 82616 66449 40644",
"2016 1 Covasna 9968 13420 19471 33198 50253 54457 70380 76294 62708 62145 47462 20722",
"2016 1 Dambovita 11828 15048 17687 18672 17511 23938 28383 31308 25766 25548 21302 14296",
"2016 1 Dolj 11918 12940 14993 14765 14581 15867 14351 15197 17281 13502 15957 12803",
"2016 1 Galati 8344 8998 11480 10779 13687 15384 11920 13481 13627 13191 13725 11248",
"2016 1 Giurgiu 2395 3054 4381 3596 4449 4456 5252 4945 4303 4145 3901 2753",
"2016 1 Gorj 10705 10234 7050 8029 9808 10785 22507 25304 13468 10956 7970 12030",
"2016 1 Harghita 27934 28557 18650 23667 45469 46631 59119 74185 43571 32800 26915 29205",
"2016 1 Hunedoara 13241 19746 21000 22042 26801 30206 39684 40611 34318 29976 25850 18963",
"2016 1 Ialomita 2176 3615 3214 9740 15811 22597 32507 37325 21940 18609 12310 4172",
"2016 1 Iasi 27774 32962 40251 44338 51767 48978 52999 47035 53867 58362 50259 36217",
"2016 1 Ilfov 12019 13083 15188 16160 19872 19706 20138 19184 20684 19707 17043 13139",
"2016 1 Maramures 18678 20053 19018 23374 28010 31954 40322 47356 30510 22603 19694 28230",
"2016 1 Mehedinti 8252 9011 12008 12827 15600 18504 27339 33146 22097 16418 14241 10281",
"2016 1 Mures 57344 64077 57300 76192 86942 88793 131481 147278 102413 86745 71110 61178",
"2016 1 Neamt 14238 14021 17163 25910 32157 47636 69266 80449 43301 31152 24132 22582",
"2016 1 Olt 4598 5766 6514 6090 5748 6495 6042 6426 5924 6943 7026 5192",
"2016 1 Prahova 79802 79909 58127 69814 86260 94508 115139 141131 108161 92907 81912 91496",
"2016 1 SatuMare 11079 10573 11084 10903 10928 11209 11680 12484 9330 9715 10161 8463",
"2016 1 Salaj 3098 4915 6314 6696 7390 7261 8551 11573 8709 7651 7141 4837",
"2016 1 Sibiu 42030 41906 38763 55642 65631 76028 105384 115093 89235 68588 58367 67415",
"2016 1 Suceava 37606 35435 29748 38042 64191 71936 94148 113875 78986 65020 47581 56847",
"2016 1 Teleorman 880 1025 1404 1561 1662 1425 1963 2440 1286 1552 1787 1037",
"2016 1 Timis 50882 58162 74121 67627 83460 78822 84882 78098 87023 86549 77351 58917",
"2016 1 Tulcea 1684 2428 2711 4677 13946 24381 24542 40050 22957 6562 2921 2939",
"2016 1 Vaslui 2477 4001 4220 4638 4808 4755 5837 6485 6061 5833 3978 3820",
"2016 1 Valcea 36123 47500 40970 72906 103659 130306 177425 192586 141023 120138 94009 50641",
"2016 1 Vrancea 4404 4385 5811 5692 6620 6676 10642 9240 5009 4912 5011 4725",
"2016 1 Bucuresti 185587 209212 243526 256148 302614 284761 272044 269805 314162 308559 272237 231315",
// 2017
"2017 1 Alba 18818 18963 20316 23545 32003 36216 48333 49561 32126 23879 19891 20203",
"2017 1 Arad 16913 17913 21474 26755 30320 35972 39419 44973 39319 37050 30590 22696",
"2017 1 Arges 20010 21960 21334 23180 25543 35626 49884 52260 38594 28676 22550 26344",
"2017 1 Bacau 17843 23831 23403 22608 24889 33749 42627 47458 37842 37281 27786 20503",
"2017 1 Bihor 50762 61590 57942 82397 91320 123630 166714 199335 146901 122442 111183 78428",
"2017 1 BistritaNasaud 8632 8190 10068 13331 21093 25165 31319 35419 23991 15294 13318 10252",
"2017 1 Botosani 6043 5805 6340 6706 7806 9059 9096 9350 9423 7685 7684 6957",
"2017 1 Brasov 220450 205774 134349 162826 175894 204871 262599 327714 223741 189998 158800 233563",
"2017 1 Braila 6903 7934 16204 14740 17512 26579 31861 31626 26003 25306 21152 11742",
"2017 1 Buzau 7969 8636 10410 13429 15520 19709 23419 25806 21957 21179 17449 11315",
"2017 1 CarasSeverin 24585 26340 23400 38830 58055 85990 105553 109751 75209 62734 49942 30652",
"2017 1 Calarasi 2957 2912 3729 4254 5362 4760 5427 5460 4938 4867 3404 2610",
"2017 1 Cluj 59260 65516 87562 105180 110121 116792 134585 136382 114084 111916 97167 70110",
"2017 1 Constanta 28127 43720 51264 73181 141453 569151 1518123 1660255 466662 74443 62790 40017",
"2017 1 Covasna 13576 12542 19200 29106 41042 66018 72856 71352 61036 61191 50601 29489",
"2017 1 Dambovita 9733 9538 16428 20251 21235 28079 31754 32622 27432 24717 23021 17450",
"2017 1 Dolj 15635 17271 19711 18810 19701 18780 18271 19172 20594 19183 19015 15046",
"2017 1 Galati 7495 8900 11755 13641 14989 16425 15001 15576 15956 16603 16470 10312",
"2017 1 Giurgiu 3559 3732 5569 4460 4617 4822 4624 5899 4419 4251 4779 3376",
"2017 1 Gorj 12218 13981 9118 11109 10833 15694 22415 29352 15822 12617 12448 14756",
"2017 1 Harghita 25549 26502 19238 27911 30600 50187 69299 79129 46560 37923 28334 31341",
"2017 1 Hunedoara 18700 17604 19972 23840 29329 33080 43048 43322 32261 26815 23678 23698",
"2017 1 Ialomita 1623 2751 7085 8990 12504 18364 28769 24911 25327 17441 4667 5297",
"2017 1 Iasi 31507 33497 44973 48426 51929 53337 50100 47367 47236 57275 46424 35697",
"2017 1 Ilfov 16219 17006 18576 19707 23446 22883 23635 21033 20657 22848 19774 14610",
"2017 1 Maramures 25117 22024 17922 24922 33403 33011 43656 56146 32115 28218 22225 33683",
"2017 1 Mehedinti 8918 8492 12539 18285 23598 25962 29657 34740 24801 16129 14767 9381",
"2017 1 Mures 54220 60065 60036 71462 76587 108843 135306 157659 102249 94517 85284 69017",
"2017 1 Neamt 16462 14844 16814 26775 39308 50903 70162 72177 40901 29524 21840 23188",
"2017 1 Olt 5763 4574 6450 5464 7392 6592 7157 7805 7527 7528 6673 6033",
"2017 1 Prahova 86198 80167 60577 67552 79131 100597 124245 148584 109116 95599 84993 100598",
"2017 1 SatuMare 9404 8491 8741 9507 9942 11863 13675 14744 12835 9908 9889 9619",
"2017 1 Salaj 3315 4651 7177 6940 9717 9185 10352 11779 8128 7834 7438 5679",
"2017 1 Sibiu 41568 44184 45568 58974 78675 92868 123314 133099 87935 69291 62981 74636",
"2017 1 Suceava 48492 42595 30181 46911 49912 71538 103851 124863 83751 67482 53974 66368",
"2017 1 Teleorman 1083 907 900 935 2067 2831 2161 2434 1793 1595 1050 939",
"2017 1 Timis 51628 52731 65963 68600 79637 85679 88119 88861 95729 86467 77588 58638",
"2017 1 Tulcea 2010 2456 3618 6967 16609 28096 37391 43617 29614 14124 14782 11064",
"2017 1 Vaslui 3122 3524 5046 5037 6224 6406 4958 9431 7013 6604 5387 4657",
"2017 1 Valcea 37501 43799 45602 60031 66547 126631 191482 204158 140756 147225 116601 73005",
"2017 1 Vrancea 3574 3957 3649 4646 4431 4233 6234 8161 4630 4861 4646 3904",
"2017 1 Bucuresti 198771 220958 272551 267593 311597 307187 297703 293667 324232 327522 285072 253392",
// 2018
"2018 1 Alba 17186",
"2018 1 Arad 19427",
"2018 1 Arges 21970",
"2018 1 Bacau 15972",
"2018 1 Bihor 66520",
"2018 1 BistritaNasaud 8199",
"2018 1 Botosani 5536",
"2018 1 Brasov 233925",
"2018 1 Braila 7867",
"2018 1 Buzau 9556",
"2018 1 CarasSeverin 21879",
"2018 1 Calarasi 2786",
"2018 1 Cluj 63377",
"2018 1 Constanta 33051",
"2018 1 Covasna 15861",
"2018 1 Dambovita 14582",
"2018 1 Dolj 14715",
"2018 1 Galati 8999",
"2018 1 Giurgiu 4356",
"2018 1 Gorj 17455",
"2018 1 Harghita 29433",
"2018 1 Hunedoara 19101",
"2018 1 Ialomita 2119",
"2018 1 Iasi 27700",
"2018 1 Ilfov 16733",
"2018 1 Maramures 30728",
"2018 1 Mehedinti 7223",
"2018 1 Mures 66680",
"2018 1 Neamt 15595",
"2018 1 Olt 6636",
"2018 1 Prahova 98880",
"2018 1 SatuMare 8065",
"2018 1 Salaj 3187",
"2018 1 Sibiu 47989",
"2018 1 Suceava 46130",
"2018 1 Teleorman 872",
"2018 1 Timis 53593",
"2018 1 Tulcea 10650",
"2018 1 Vaslui 3676",
"2018 1 Valcea 47998",
"2018 1 Vrancea 4849",
"2018 1 Bucuresti 210507",

                              };

            var items = GetItems<NumberOfNights>(rawData);

            return items;
        }
    }
}