﻿using System.Collections.Generic;
using StatisticsRomania.BusinessObjects;

namespace StatisticsRomania.Repository.Seeders
{
    internal class ExportFobSeeder : BaseSeeder
    {
        internal static List<ExportFob> GetData()
        {
            var rawData = new List<string>()
                              {
                                  // 2014, 2015
                                  "2014 10 Alba 89420 87054 83949 86022 79499 102731 88834 88436 92635 92652 73633 77977 86215 87326 67581",
                                  "2014 10 Arad 266767 266887 192009 233753 258932 272701 261730 253111 271469 273630 196922 294088 297293 287158 215092",
                                  "2014 10 Arges 486602 497824 363562 397901 433232 477330 432647 469465 469853 472480 270302 481285 519605 482418 365826",
                                  "2014 10 Bacau 41145 41462 30591 33024 35682 42472 38784 40532 42214 47635 31638 41329 42920 41380 35478",
                                  "2014 10 Bihor 187915 185874 141073 145082 147545 174629 145370 145295 158657 163638 141256 174630 188087 173999 137474",
                                  "2014 10 BistritaNasaud 61106 58883 43890 52147 53329 58880 53772 57778 58625 62650 47516 64577 71323 67130 48134",
                                  "2014 10 Botosani 24431 21864 18150 22480 26581 27523 24710 25582 24643 27669 17221 22208 25951 23838 21034",
                                  "2014 10 Brasov 240786 217692 176219 213882 216595 251660 240574 223283 246948 237533 195352 241370 272712 253715 195307",
                                  "2014 10 Braila 22408 17689 25339 10182 30100 14945 12898 28369 33845 31731 20066 12593 13034 10683 29246",
                                  "2014 10 Buzau 78846 62270 36532 56287 45196 40096 45196 47809 50386 59847 64600 55686 50789 57484 37322",
                                  "2014 10 CarasSeverin 25484 23962 18485 26163 26404 28428 26919 23801 25879 28014 19090 26987 29149 28887 16447",
                                  "2014 10 Calarasi 35339 27498 18616 22590 22075 29612 23966 26140 31996 41518 31313 29372 28279 19612 23046",
                                  "2014 10 Cluj 104444 96042 73181 83381 91340 97005 92306 102606 103444 100167 89020 107804 117454 111434 85759",
                                  "2014 10 Constanta 235941 181794 145227 123859 136960 157115 154407 151514 249376 211225 161052 289869 153506 87188 155948",
                                  "2014 10 Covasna 28397 24500 22289 26069 28730 28865 25869 18917 25785 27361 23627 23780 24295 24028 18123",
                                  "2014 10 Dambovita 54207 48118 38045 43102 44717 53018 49946 52486 57773 62249 51583 61084 56787 49407 40459",
                                  "2014 10 Dolj 73429 74026 80369 113684 84195 102390 79659 87384 103132 96749 72189 105316 73492 99290 79398",
                                  "2014 10 Galati 65254 72575 131795 77231 78619 69444 102857 73003 66225 73440 73539 57941 70339 51683 53085",
                                  "2014 10 Giurgiu 6629 5036 2734 6168 3327 5147 4704 5475 4789 4796 3664 4754 5456 4741 3901",
                                  "2014 10 Gorj 4887 3974 5368 3082 3776 5725 4054 5332 3835 5503 4726 3933 5019 5310 3389",
                                  "2014 10 Harghita 29294 26608 22047 21964 24087 26509 23080 26843 26592 29204 22136 27504 28893 26273 20304",
                                  "2014 10 Hunedoara 70638 62060 46658 58086 63418 73996 63622 65008 63545 72295 45021 66075 68525 67540 54946",
                                  "2014 10 Ialomita 19456 18240 14592 12293 14128 17537 17642 15104 22260 22678 14012 24356 18573 14271 14671",
                                  "2014 10 Iasi 63952 64743 52376 56663 56085 69028 69999 60472 68116 67104 52055 68821 71032 64435 58780",
                                  "2014 10 Ilfov 197520 202128 169542 139196 206201 209624 181202 192886 204717 224730 186192 207162 222422 217419 177813",
                                  "2014 10 Maramures 98738 91921 72742 84644 84866 89071 83025 89997 89875 96694 83572 95338 101504 97576 80567",
                                  "2014 10 Mehedinti 8896 7715 10839 6130 9938 9048 8771 10634 9911 10550 3993 11422 10188 8665 5064",
                                  "2014 10 Mures 82939 77364 64893 58492 75545 79452 73138 78404 78907 88708 72997 87151 86563 94304 62793",
                                  "2014 10 Neamt 38283 31675 31709 31514 31982 37337 31364 32746 36522 38903 26151 34150 39197 39064 29392",
                                  "2014 10 Olt 113637 116917 87468 100438 97273 113095 113802 104942 108250 103401 96739 109266 110823 103604 84157",
                                  "2014 10 Prahova 149827 161025 157017 138295 156979 184049 123807 170073 187196 195065 144667 167946 154135 148902 139253",
                                  "2014 10 SatuMare 76038 69309 59116 65895 69217 77708 69785 71838 73746 72891 69553 73741 80354 77292 60175",
                                  "2014 10 Salaj 50316 58774 29971 40247 40571 40084 41465 36375 44046 35529 33691 39871 42280 47914 49414",
                                  "2014 10 Sibiu 215191 186754 147259 196175 193189 202792 186798 191957 201014 201831 156541 197627 214898 207792 153737",
                                  "2014 10 Suceava 40573 34126 27787 29185 33194 38336 34149 35987 34612 39236 32547 33810 37004 35827 31087",
                                  "2014 10 Teleorman 27495 26746 24903 22939 16820 20391 16885 11941 12606 13447 7928 11986 13226 37058 10529",
                                  "2014 10 Timis 452386 408177 299170 346195 433307 485134 438924 417967 450303 481943 363852 487767 479108 463963 358312",
                                  "2014 10 Tulcea 10674 11163 55173 33368 8459 9558 10449 66213 8605 64351 11500 13998 68564 98580 56862",
                                  "2014 10 Vaslui 15226 13240 10843 14764 13935 14900 13177 13001 16451 18573 13379 14692 14590 14116 11932",
                                  "2014 10 Valcea 32432 29219 26832 24162 28511 31936 32486 31238 35063 35494 30338 34700 34827 31641 25249",
                                  "2014 10 Vrancea 28022 22225 16522 19319 18298 18283 15629 21898 24325 29350 27093 21548 25291 22964 20433",
                                  "2014 10 Bucuresti 858381 825242 638556 807101 765245 827632 761463 722803 769572 888355 744411 834822 853472 770956 696000",
                                  // 2016
"2016 1 Alba 82110 76063 96808 87551 91599 111128 109819 108745 145791 134473 151114 126852",
"2016 1 Arad 255447 279973 303773 292941 273783 288997 257969 242231 301509 284028 306355 217652",
"2016 1 Arges 435612 476147 545232 528482 526704 533762 454204 289198 508830 527505 526505 437148",
"2016 1 Bacau 37955 39592 42992 43668 43145 45443 44583 34781 45145 38017 43646 31488",
"2016 1 Bihor 151169 151020 156177 141238 144077 150207 149741 136603 166024 147995 157996 127421",
"2016 1 BistritaNasaud 54948 63701 67490 73175 62701 70251 69848 59162 70408 71258 75555 53082",
"2016 1 Botosani 25523 28714 27247 24572 24252 26255 27503 18731 27702 24422 25990 23053",
"2016 1 Brasov 219432 249453 263164 253648 240392 251769 228703 218254 270152 251083 274483 185949",
"2016 1 Braila 11246 12083 12193 24516 11759 12404 12392 27428 10980 19037 9973 27593",
"2016 1 Buzau 33142 40041 42187 44986 43351 45744 45514 60484 62443 61289 57444 48462",
"2016 1 CarasSeverin 28558 28373 28969 29345 27621 28294 22512 28956 30250 32409 31656 19673",
"2016 1 Calarasi 26714 34644 32532 24114 25234 34386 60603 32942 33420 21719 37832 25262",
"2016 1 Cluj 90350 100836 107874 103240 98092 104769 101957 101014 122136 122942 131492 103533",
"2016 1 Constanta 91682 209475 111364 151278 231956 152416 112673 258458 107118 182849 201173 147407",
"2016 1 Covasna 24181 28299 26763 24677 23872 26753 28272 26037 24825 24409 26400 20057",
"2016 1 Dambovita 42243 49493 57130 54768 55272 60329 51165 53576 54445 51859 52963 41034",
"2016 1 Dolj 90913 152676 102462 91299 83232 91307 85358 65120 112694 65196 79204 58956",
"2016 1 Galati 47990 97484 53698 52781 60166 93981 94345 58590 63898 71862 72768 71679",
"2016 1 Giurgiu 3350 5572 4441 10545 5633 5168 4558 3505 10139 4886 6091 4269",
"2016 1 Gorj 3137 3546 4550 3349 2663 5013 4496 4710 5579 4559 4834 3675",
"2016 1 Harghita 22841 23738 26811 24958 25633 25556 23648 22910 28579 28237 26452 20743",
"2016 1 Hunedoara 58700 71198 80927 69869 66491 64583 60095 49603 75229 65441 71752 48143",
"2016 1 Ialomita 12374 13323 15032 17750 14129 13041 16572 15174 15065 15491 23146 16669",
"2016 1 Iasi 57561 67477 79233 69878 73574 76911 62271 61609 66998 62662 76367 59685",
"2016 1 Ilfov 174746 209474 213304 201209 174662 171341 218143 183155 211695 210898 202827 191685",
"2016 1 Maramures 89349 95767 106028 94554 97448 102209 104595 105560 111329 108618 115902 85098",
"2016 1 Mehedinti 6850 12746 12005 8263 11467 8512 12187 6538 10303 11473 11355 6029",
"2016 1 Mures 77750 80716 85443 84128 85800 88149 78681 72136 81414 88720 78375 66085",
"2016 1 Neamt 29143 34444 37169 34913 33173 38361 34808 26207 35891 32985 33336 24789",
"2016 1 Olt 92365 107161 111369 108106 107371 108660 101575 97067 104554 109926 112711 85323",
"2016 1 Prahova 144676 171881 208294 160809 167143 180482 151246 151705 171429 166131 190479 183865",
"2016 1 SatuMare 72944 77359 79178 76217 77587 79739 72432 75575 83916 80955 85149 63431",
"2016 1 Salaj 44247 31487 32855 32392 33261 35461 27607 31806 43417 41486 41393 37230",
"2016 1 Sibiu 213539 223712 238075 228874 216595 230422 216031 204583 244455 235252 252696 183834",
"2016 1 Suceava 30779 33655 38588 34152 35859 37302 36531 32819 42879 36608 39148 31918",
"2016 1 Teleorman 14320 13037 13852 12102 11465 14355 12278 7826 11474 11002 12315 9614",
"2016 1 Timis 417952 486404 519885 478099 469353 513238 495171 472416 550441 523575 565562 415368",
"2016 1 Tulcea 9453 8826 13512 46582 7609 39562 42616 13200 42257 18568 15978 29684",
"2016 1 Vaslui 13387 16156 13999 13220 13058 15602 15505 12790 14154 13613 14307 10586",
"2016 1 Valcea 29446 32149 33304 31616 34696 37113 31480 29351 34626 33376 35844 29218",
"2016 1 Vrancea 18726 21617 17919 18607 18175 21441 25606 23509 26589 20008 24061 21341",
"2016 1 Bucuresti 635947 750539 770153 727758 744608 802784 906959 795726 949897 928042 907861 811858",
// 2017
"2017 1 Alba 181036 222950",
"2017 1 Arad 280545 283830",
"2017 1 Arges 491632 502426",
"2017 1 Bacau 41070 38890",
"2017 1 Bihor 128184 141777",
"2017 1 BistritaNasaud 66151 67313",
"2017 1 Botosani 23764 28598",
"2017 1 Brasov 245330 261715",
"2017 1 Braila 8130 9844",
"2017 1 Buzau 51527 43329",
"2017 1 CarasSeverin 25424 26992",
"2017 1 Calarasi 26605 37127",
"2017 1 Cluj 111044 117930",
"2017 1 Constanta 146339 137908",
"2017 1 Covasna 28103 28547",
"2017 1 Dambovita 45185 57541",
"2017 1 Dolj 91923 113693",
"2017 1 Galati 97164 79597",
"2017 1 Giurgiu 3645 5291",
"2017 1 Gorj 3053 3372",
"2017 1 Harghita 23339 23388",
"2017 1 Hunedoara 64572 69390",
"2017 1 Ialomita 12159 20190",
"2017 1 Iasi 62605 73769",
"2017 1 Ilfov 162680 204766",
"2017 1 Maramures 100597 106331",
"2017 1 Mehedinti 7988 9351",
"2017 1 Mures 70224 67318",
"2017 1 Neamt 32336 35235",
"2017 1 Olt 113178 117484",
"2017 1 Prahova 156681 170589",
"2017 1 SatuMare 78199 81053",
"2017 1 Salaj 41557 54887",
"2017 1 Sibiu 231856 245265",
"2017 1 Suceava 31384 38989",
"2017 1 Teleorman 14378 12262",
"2017 1 Timis 477443 523293",
"2017 1 Tulcea 7878 8720",
"2017 1 Vaslui 15844 13620",
"2017 1 Valcea 32772 34724",
"2017 1 Vrancea 17458 19188",
"2017 1 Bucuresti 734390 814672",
                              };

            var items = GetItems<ExportFob>(rawData);

            return items;
        }
    }
}
