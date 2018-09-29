﻿using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class ImportCifSeeder : BaseSeeder
    {
        internal static List<ImportCif> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 48212 47661 45107 35758 48916 58239 46052 54023 52484 53825 44984 51495 53099 50993 58162",
                                  "2014 10 Arad 230502 213119 177039 193974 209168 246185 226557 223233 239478 243639 198076 247669 253032 237445 186185",
                                  "2014 10 Arges 365805 323459 272226 314047 323744 354820 332661 322119 348530 356185 216594 378952 366901 334302 301696",
                                  "2014 10 Bacau 42854 39482 29312 32916 49463 55251 49980 48582 57307 54469 43033 55893 55412 51793 43054",
                                  "2014 10 Bihor 190601 179009 135398 153467 157936 167516 161205 156043 159487 164475 138536 177700 184295 175206 151829",
                                  "2014 10 BistritaNasaud 48727 47728 38951 43257 42021 48548 42463 49818 57170 55184 43163 61716 63671 58160 51210",
                                  "2014 10 Botosani 18923 23098 19131 18274 17872 22718 23049 23448 23076 23848 12935 22269 22141 23000 20059",
                                  "2014 10 Brasov 229691 217409 195364 181512 211836 228814 207513 201402 223538 238106 202282 236818 239401 221042 200411",
                                  "2014 10 Braila 18015 15449 16678 15037 10835 13171 14171 14516 20284 14929 11072 13836 15607 13361 12615",
                                  "2014 10 Buzau 41460 43900 29129 36100 27639 51764 41054 29219 49327 59158 45889 38131 51415 33519 75750",
                                  "2014 10 CarasSeverin 18652 16607 12676 16378 16521 19041 18064 16441 19210 20019 17388 17821 20007 19892 18983",
                                  "2014 10 Calarasi 22067 21875 15915 16382 22622 28609 30201 22569 29099 33164 20525 27331 17699 18894 15055",
                                  "2014 10 Cluj 184009 172960 155099 124955 139789 171016 167207 168602 171560 189104 160034 191642 202734 184686 173936",
                                  "2014 10 Constanta 308698 236836 237810 161989 190021 247099 248040 253420 211112 346237 240896 238791 166339 107485 188399",
                                  "2014 10 Covasna 27279 26166 21076 19989 22083 25013 22504 22069 23737 22016 21133 25675 25716 24044 21748",
                                  "2014 10 Dambovita 41979 36813 26673 31572 32879 41923 41457 42329 50905 51085 48271 43661 49802 44336 42432",
                                  "2014 10 Dolj 81666 66199 62881 73673 88189 85899 68741 75009 84669 86328 57271 93501 82772 75385 67655",
                                  "2014 10 Galati 78819 87217 66092 69169 69201 69137 66716 81859 80201 87993 90794 76560 82983 81288 82155",
                                  "2014 10 Giurgiu 12606 10010 7508 5926 7661 10769 7531 9062 15374 10026 7065 10009 9438 10406 9965",
                                  "2014 10 Gorj 6103 3602 4616 4536 3824 4061 4576 3636 3990 3870 4207 4226 3411 4894 2872",
                                  "2014 10 Harghita 37859 28059 23854 22825 24228 33727 29842 30418 35473 33607 28792 37205 34668 35333 27430",
                                  "2014 10 Hunedoara 48368 47998 46146 45925 51741 62097 50104 43677 46024 49175 35136 50868 51860 50711 46036",
                                  "2014 10 Ialomita 12181 10048 7619 7523 9537 11714 11979 11585 16247 14839 15718 14826 12683 18213 20243",
                                  "2014 10 Iasi 55714 55075 43183 42600 50970 64927 58432 57076 68986 65508 56766 68578 71161 65113 67406",
                                  "2014 10 Ilfov 389247 373346 350331 302308 348048 414896 384950 375771 423107 432340 374812 441123 450367 447786 428074",
                                  "2014 10 Maramures 71409 66193 52631 52558 59172 66274 63168 61178 71784 72415 58275 77134 75459 81627 64387",
                                  "2014 10 Mehedinti 7544 6375 4868 6570 7389 7992 7213 8419 9228 8534 5024 9485 7866 8446 4718",
                                  "2014 10 Mures 89008 89482 95169 81675 78673 102642 85213 81286 97161 110718 77233 96273 114651 94726 91950",
                                  "2014 10 Neamt 26781 24098 24437 19530 26396 37128 30341 28016 38564 30855 27596 27672 32516 30054 24861",
                                  "2014 10 Olt 62789 55542 38984 47747 48722 63553 51812 49191 58944 59439 47701 52811 48136 50719 41314",
                                  "2014 10 Prahova 261010 247410 247664 171846 235883 270786 164968 229257 242861 240376 230968 245383 252705 237126 216417",
                                  "2014 10 SatuMare 76828 72828 60678 60287 66322 76254 73766 68066 74160 82293 80937 87223 87397 78948 65764",
                                  "2014 10 Salaj 27934 22869 22368 30361 24426 25495 21484 21842 23430 28999 16824 23050 26350 21836 17518",
                                  "2014 10 Sibiu 179068 162919 137582 148423 152920 171377 158236 148387 162867 167563 136281 163879 192477 168466 141962",
                                  "2014 10 Suceava 37114 31266 30969 31530 36057 41700 40382 39149 45563 48198 40333 47538 46334 42929 40563",
                                  "2014 10 Teleorman 11394 11505 9800 10153 9813 11787 13182 26030 17653 16450 11761 15051 13154 10816 10613",
                                  "2014 10 Timis 386957 354973 304349 267731 356230 395190 368990 362601 394173 413288 332114 403307 420650 388238 352534",
                                  "2014 10 Tulcea 21883 23123 19733 21408 24645 33046 24988 34762 21179 24958 15038 21450 17189 19389 16188",
                                  "2014 10 Vaslui 14326 10589 9592 9311 10458 10926 13244 14201 15074 13242 7601 11615 11494 10891 9750",
                                  "2014 10 Valcea 23490 20412 16957 15247 21572 25514 23051 19851 23482 25248 21092 25228 22519 22121 23560",
                                  "2014 10 Vrancea 20157 15635 14718 15473 17634 20556 23160 20208 25024 20479 13983 22138 22977 18235 16131",
                                  "2014 10 Bucuresti 1586809 1517911 1400884 1373561 1406178 1687312 1503886 1461430 1600810 1586486 1490970 1679278 1868318 1827888 1631570",
                                  // 2016
"2016 1 Alba 47767 57887 60520 61974 65397 62505 68946 66042 85578 90091 96713 113033",
"2016 1 Arad 209918 254330 270416 246893 252250 259097 227792 229090 264527 250415 248779 211745",
"2016 1 Arges 320133 357075 392423 380415 396492 350279 351754 234356 385076 413516 377730 327035",
"2016 1 Bacau 39888 54368 67925 61393 62584 62489 58275 58445 64606 59670 56554 49244",
"2016 1 Bihor 143186 152731 155104 154175 158608 158189 163775 134604 156936 156252 158055 122241",
"2016 1 BistritaNasaud 49925 57841 60586 53592 57819 60334 51372 49431 63145 62458 59876 48590",
"2016 1 Botosani 15161 18883 22263 21584 23129 21726 21467 15429 21056 23957 24304 19359",
"2016 1 Brasov 191629 225175 248061 233529 228432 235650 229654 211903 237802 254290 256420 220386",
"2016 1 Braila 9165 12422 17137 16158 17144 18020 16867 14256 14944 13514 12121 11383",
"2016 1 Buzau 28855 32355 35944 37355 36857 42471 51136 31314 28576 28664 32181 28305",
"2016 1 CarasSeverin 16577 17317 18793 18083 20429 21290 16507 22040 22799 24508 23926 16598",
"2016 1 Calarasi 15262 18215 23337 17814 22912 19874 20743 24434 17072 20409 20827 20710",
"2016 1 Cluj 135340 163757 185474 172526 173184 178915 166875 175759 195429 197446 192639 171041",
"2016 1 Constanta 109360 154022 197679 175492 217029 198562 196620 304129 148078 135296 208825 220438",
"2016 1 Covasna 17612 24067 24871 27590 22812 27278 24449 22645 27008 28086 26056 26770",
"2016 1 Dambovita 33010 43301 50550 44139 48411 48912 48885 49860 50746 50458 46453 40416",
"2016 1 Dolj 71934 89464 92831 81426 79742 69467 61334 65940 80374 67277 70289 58559",
"2016 1 Galati 48682 60411 86202 61554 61735 68409 64713 68167 74782 87465 75346 88870",
"2016 1 Giurgiu 6778 8642 13546 12121 15523 14589 12018 9269 12776 11037 11634 12770",
"2016 1 Gorj 2795 3217 3098 4280 4095 3864 3592 3204 4709 4352 4759 3850",
"2016 1 Harghita 23953 31349 34012 33157 39146 34792 30236 29709 34807 36935 37163 27664",
"2016 1 Hunedoara 46326 58777 62438 52293 48601 50282 47063 43153 48792 55049 57180 47510",
"2016 1 Ialomita 9493 14012 16585 15542 17971 19620 17878 15449 16791 12525 19629 17101",
"2016 1 Iasi 48188 66467 69863 70082 63898 61581 58580 70771 65210 71528 73140 66505",
"2016 1 Ilfov 340309 437397 527533 473186 465210 475591 451396 460833 497968 531637 525412 456583",
"2016 1 Maramures 60372 74481 78372 76773 78692 77382 75090 72701 83971 84023 81874 70882",
"2016 1 Mehedinti 6069 7362 7371 7648 9071 7372 7519 5713 8622 8768 9402 7035",
"2016 1 Mures 84948 99060 91171 104839 101592 95713 98040 90342 99777 124261 117231 114905",
"2016 1 Neamt 25562 33761 31766 32816 33352 33185 31991 27608 32561 33503 31921 28267",
"2016 1 Olt 48992 57959 59673 47806 52804 53268 56424 55014 59180 54734 51704 46036",
"2016 1 Prahova 190565 222000 242042 244470 243633 265887 239300 238995 255545 256160 267299 262848",
"2016 1 SatuMare 66847 82737 85022 84694 78502 80442 76689 84021 90288 90286 85160 66844",
"2016 1 Salaj 19772 22818 23662 23976 20864 21255 17733 21412 28028 24038 24588 23073",
"2016 1 Sibiu 150341 175896 178261 171633 173650 180328 163830 159477 186036 181555 191441 151345",
"2016 1 Suceava 39066 51884 52695 50298 47601 47111 46861 45896 47949 49611 44030 45203",
"2016 1 Teleorman 10685 13615 14674 13038 11411 14280 12215 13429 15093 13671 13742 12575",
"2016 1 Timis 365605 398309 427781 421027 417317 427601 420015 398759 462754 462192 473771 399754",
"2016 1 Tulcea 11619 15213 17326 20962 22748 22342 18528 13593 18221 16530 14849 17421",
"2016 1 Vaslui 9981 9505 11562 11578 13194 13505 10322 8680 11472 10425 10073 9037",
"2016 1 Valcea 16653 21585 24646 23498 24954 21866 21786 18800 26305 23772 23351 25130",
"2016 1 Vrancea 12797 19401 23183 23971 23882 24247 22845 21839 22129 24962 21050 18572",
"2016 1 Bucuresti 1362776 1665162 1756254 1627526 1669249 1654090 1635556 1743164 1903332 1876742 1967589 1735378",
// 2017
"2017 1 Alba 83516 91958 142254 131684 137243 112214 114912 114898 121216 132802 111892 118114",
"2017 1 Arad 238043 253779 289022 240080 293325 266803 247476 245320 272906 276338 268298 215830",
"2017 1 Arges 344639 351083 407969 360486 413335 405044 374621 284177 414931 470833 424476 377003",
"2017 1 Bacau 45267 52154 76393 63216 75136 67699 66594 63835 75192 74008 65179 60584",
"2017 1 Bihor 131732 139369 172922 151509 171092 160969 157513 137009 151914 165835 163541 127812",
"2017 1 BistritaNasaud 52618 54563 58626 46436 55955 59733 53731 48678 66025 60457 57199 46568",
"2017 1 Botosani 20040 21409 26872 23209 30132 23972 23448 17555 26916 25666 26568 22329",
"2017 1 Brasov 211939 233225 262953 232720 277227 261012 249330 241074 262381 285000 269608 224063",
"2017 1 Braila 9507 13714 20773 17542 23923 20765 17384 14565 13895 19015 18932 16943",
"2017 1 Buzau 25563 29873 38971 33228 72963 42496 32976 35151 33889 33057 38733 34335",
"2017 1 CarasSeverin 17853 17315 19850 17340 20952 19529 16210 17610 19171 21852 21527 19419",
"2017 1 Calarasi 18612 18676 31297 23380 23976 27722 25162 25295 24643 21360 26354 21037",
"2017 1 Cluj 169998 185664 222192 194218 220490 208470 202621 215897 242971 254740 224682 195882",
"2017 1 Constanta 230143 180197 200219 212504 226725 156498 269012 231522 198414 270105 242898 269426",
"2017 1 Covasna 21528 24834 27724 21941 28359 22573 24559 23371 26152 29491 28580 22842",
"2017 1 Dambovita 38356 48380 54086 50932 58197 55926 53531 58810 58761 68119 52777 47217",
"2017 1 Dolj 84158 96030 120813 81928 113627 117137 106999 95780 70276 88483 118170 132341",
"2017 1 Galati 69249 87158 130203 96335 105828 104961 100975 101960 117812 123420 107839 90778",
"2017 1 Giurgiu 10926 15207 13906 13009 14907 15108 13467 12425 15318 15351 14196 11877",
"2017 1 Gorj 3338 3445 4109 2847 4353 3788 3668 3982 4496 3501 4743 4564",
"2017 1 Harghita 25762 29232 36211 33119 38687 39300 35588 31808 39906 44000 43666 26961",
"2017 1 Hunedoara 48859 58050 64914 55957 56954 57755 48637 42050 53657 58950 57913 38751",
"2017 1 Ialomita 12401 18684 19277 17497 20609 22286 21373 19240 25590 15390 18999 19021",
"2017 1 Iasi 58032 71757 71468 72124 77388 75577 76427 66647 81434 88219 81426 71115",
"2017 1 Ilfov 412853 480320 564450 522185 576384 735244 554602 539475 595658 599027 591230 517615",
"2017 1 Maramures 69834 73300 85016 78200 85259 82686 80601 79073 92998 94273 88747 74425",
"2017 1 Mehedinti 6123 8116 9623 7614 10777 9265 7726 4632 8432 11476 11544 9640",
"2017 1 Mures 118963 106376 113196 98854 99465 109095 93587 115295 98653 114177 108814 108774",
"2017 1 Neamt 16462 30984 35441 33473 39903 35242 34050 33903 36536 39224 37976 29016",
"2017 1 Olt 50168 60820 73595 65262 72519 67008 73773 64459 70867 73770 68979 59699",
"2017 1 Prahova 257681 255676 231871 271493 306743 278652 276838 272412 283846 302928 308808 288669",
"2017 1 SatuMare 75752 75453 91203 78920 96020 78839 76559 81797 92443 93677 91715 66767",
"2017 1 Salaj 21739 24821 26989 24710 31012 28052 23062 23979 28433 32153 31003 20993",
"2017 1 Sibiu 178711 194392 224144 177846 209078 203283 196615 174976 201644 200744 203162 177836",
"2017 1 Suceava 35695 45065 55031 48043 54626 58110 54233 51684 58962 62269 55084 50693",
"2017 1 Teleorman 9872 15568 17925 13162 13065 17402 14472 19637 20345 16287 21942 12397",
"2017 1 Timis 402924 429182 497097 427280 489218 429218 419826 399812 453162 497392 468730 408849",
"2017 1 Tulcea 11537 13351 24994 19888 19647 26065 25365 28099 23488 22625 24459 27123",
"2017 1 Vaslui 8137 10316 13140 10192 14387 13065 12664 11180 10925 12200 12809 11122",
"2017 1 Valcea 21558 27298 27931 22951 27145 26459 28035 27971 25538 30285 31821 32117",
"2017 1 Vrancea 18297 21144 26283 25183 31536 26960 21864 19003 22214 24898 22474 16450",
"2017 1 Bucuresti 1544594 1698385 2089365 1658732 1877370 1818050 1834763 1845484 1996149 2155664 2181750 1928663",
// 2018
"2018 1 Alba 173935 168635 209902 162761 169821",
"2018 1 Arad 261004 259600 283818 253222 286281",
"2018 1 Arges 388797 397528 419388 396994 427513",
"2018 1 Bacau 60079 62210 73983 67722 79908",
"2018 1 Bihor 149248 144257 155081 147936 166358",
"2018 1 BistritaNasaud 53922 56829 61956 53171 54105",
"2018 1 Botosani 21988 21605 26314 22059 25288",
"2018 1 Brasov 249363 254927 280559 251676 276071",
"2018 1 Braila 12341 12870 23662 19609 18252",
"2018 1 Buzau 37340 30131 55900 61686 39868",
"2018 1 CarasSeverin 19071 17422 19947 20212 23759",
"2018 1 Calarasi 19557 23283 28022 21992 28507",
"2018 1 Cluj 196354 205509 227694 210559 237693",
"2018 1 Constanta 260526 245932 237526 199085 334529",
"2018 1 Covasna 23532 21910 27158 22102 27744",
"2018 1 Dambovita 39499 48841 57923 47142 61166",
"2018 1 Dolj 162995 173009 194442 140017 196711",
"2018 1 Galati 97809 86969 114319 87758 94827",
"2018 1 Giurgiu 11547 15054 16285 14895 15745",
"2018 1 Gorj 2719 2710 2833 3697 5047",
"2018 1 Harghita 30664 29713 34627 33787 39845",
"2018 1 Hunedoara 52258 52070 62447 51517 56064",
"2018 1 Ialomita 11392 12411 15837 13077 14949",
"2018 1 Iasi 67420 74289 75741 72559 82393",
"2018 1 Ilfov 496969 521364 610163 585994 618848",
"2018 1 Maramures 75176 77791 88861 81479 89589",
"2018 1 Mehedinti 8010 6998 9647 6942 8378",
"2018 1 Mures 97296 105283 119036 93108 106386",
"2018 1 Neamt 28892 31658 37272 34301 37655",
"2018 1 Olt 68164 60810 75217 73892 77618",
"2018 1 Prahova 315894 282817 323519 283719 330079",
"2018 1 SatuMare 81508 82661 90683 80720 91343",
"2018 1 Salaj 28491 28049 27851 26498 33287",
"2018 1 Sibiu 204229 207946 226475 193719 216566",
"2018 1 Suceava 42452 48581 59974 50064 62932",
"2018 1 Teleorman 12060 11989 15619 15658 15358",
"2018 1 Timis 429412 471844 516812 448012 500253",
"2018 1 Tulcea 30099 25752 39657 22388 30431",
"2018 1 Vaslui 10836 10830 12618 11309 14314",
"2018 1 Valcea 23547 23009 27911 24365 30855",
"2018 1 Vrancea 18793 21994 24274 23662 28701",
"2018 1 Bucuresti 1780726 1856302 2109103 1766917 2024632",

                              };

            var items = GetItems<ImportCif>(rawData);

            return items;
        }
    }
}
