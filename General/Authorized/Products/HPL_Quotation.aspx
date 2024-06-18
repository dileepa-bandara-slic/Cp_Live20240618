<%@ Page Title="" Language="C#" MasterPageFile="~/General.master" AutoEventWireup="true"
    CodeFile="HPL_Quotation.aspx.cs" Inherits="General_Authorized_Products_HPL_Quotation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
  
    <style>
        @media (max-width:479px) {
            .navbar-fixed-top + .main-container {
                padding-top: 50px;
            }
        }
    </style>
    <style>
        
        .Table_tdplan{
            text-align: center;
        }

        .Table_tdplan_ErPos{
            padding-left:3%;
        }

        .marginga_top{
            margin-top:3px;
        }

        .Table_desc {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: left;
            padding: 7px 5px 7px 5px;
            text-align: justify; 
            text-justify: inter-word;
        }

        .Table_td {
            border: 1px solid black;
            border-collapse: collapse;
            text-align: center;
        }

		ul.b {list-style-type: square;
                 margin-left : 15px;}
        
          
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container" id="main-container"  style="min-height:600px">
        <div class="container">
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="/">Home</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/">General</a></li>
                        <li class="breadcrumb-item"><a href="/General/Authorized/General_Products.aspx">Purchase
                            online</a></li>
                        <li class="breadcrumb-item active">Home protect lite quotation</li>
                    </ol>
                </div>
                <div class="col-xs-1">
                </div>
            </div>
            <div class="row">
                <div class="col-xs-1">
                </div>
                <div class="col-xs-10">
                    <center><h3>Home Protect Lite</h3>
                    </center>
                    <h3>Procuct Scope<left></left></h3>
                </div>
                <div class="col-xs-1">
                </div>
            </div>

            <div class="row">

                    <div  class="col-md-6" style="border: 0px solid #212121; float: left; padding:7px" >
                        <p>Type - Combined Fire and Burglary Insurance “Home Insurance”</p>
                        <P><b>Cover -</b></P>
				  
                        <ul class="b">
                            <li>Fire/ Lightning</li>
                            <li>Malicious damage by any person(s)</li>
                            <li>Explosion</li>
                            <li>Natural Perils including Cyclone/ Storm/ Tempest/ Flood/ Earthquake, Tsunami</li>
                            <li>Electrical short circuiting</li>
                            <li>Bursting & overflowing of water tanks, Apparatus and pipes.</li>
                            <li>Aircraft damage</li>
                            <li>Impact Damage</li>
                            <li>Burglary involving forcible and violent entry/exit (for contents only) & 
				Damage to the building caused by burglary of house breaking or attempt thereat.
				(As fully described in the Home Protect Lite standard policy of Sri Lanka Insurance General Ltd.)
				</li>
                        </ul>

                        <br />
                        <div>
                            <table class="nav-justified Table_td">
                                <tr style="background-color:#80DEEA; font-weight:bold">
                                    <td class="Table_desc">Limits of Liability</td>
                                    <td class="Table_td">Plan 01 (Rs.)</td>
                                    <td class="Table_td">Plan 02 (Rs.)</td>
                                    <td class="Table_td ">Plan 03 (Rs.)</td>
                                    <td class="Table_td">Plan 04 (Rs.)</td>
                                    <td class="Table_td">Plan 05 (Rs.)</td>
                                </tr>
                                <tr style="background-color:#B2EBF2">
                                    <td class="Table_desc">Building, permanent fixtures and fittings</td>
                                    <td class="Table_td">0.75Mn</td>
                                    <td class="Table_td">1.5Mn</td>
                                    <td class="Table_td">2.25Mn</td>
                                    <td class="Table_td">3Mn</td>
                                    <td class="Table_td">3.75Mn</td>
                                </tr>
                                <tr style="background-color:#80DEEA">
                                    <td class="Table_desc">Contents excluding personal effects such as jewellery, money, professional equipment</td>
                                    <td class="Table_td">0.25Mn</td>
                                    <td class="Table_td">0.5Mn</td>
                                    <td class="Table_td">0.75Mn</td>
                                    <td class="Table_td">1Mn</td>
                                    <td class="Table_td">1.25Mn</td>
                                </tr>
                                <tr style="background-color:#B2EBF2">
                                    <td class="Table_desc">Sub Limit - Removal of debris</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                </tr>
                                <tr style="background-color:#80DEEA">
                                    <td class="Table_desc">Sub Limit - Professional fee for reinstating the building</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                </tr>
                                <tr style="background-color:#B2EBF2">
                                    <td class="Table_desc">Sub Limit - Burglary</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">50,000</td>
                                    <td class="Table_td">75,000</td>
                                    <td class="Table_td">100,000</td>
                                </tr>
                                <tr style="background-color:#80DEEA">
                                    <td class="Table_desc">Sub Limit - Electrical Damage cover</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">25,000</td>
                                    <td class="Table_td">50,000</td>
                                    <td class="Table_td">75,000</td>
                                    <td class="Table_td">100,000</td>
                                </tr>
                                 <tr style="background-color:#80DEEA">
                                    <td class="Table_desc"><b>Total Sum Insured</b></td>
                                    <td class="Table_td"><b>1Mn</b></td>
                                    <td class="Table_td"><b>2Mn</b></td>
                                    <td class="Table_td"><b>3Mn</b></td>
                                    <td class="Table_td"><b>4Mn</b></td>
                                    <td class="Table_td"><b>5Mn</b></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-md-6" style="border-left: 1px solid #212121; float: left; padding: 7px 15px 7px 7px;"">

                        <div>
                            <table class="nav-justified">
                                <tr style="background-color:#80CBC4; font-weight:bold">
                                    <td class="Table_desc">Plan Total annual premium (Rs.)</td>
                                    <td class="Table_td">Option 01</td>
                                    <td class="Table_td">Option 02</td>
                                    <td  class="Table_td">Option 03</td>
                                    <td class="Table_td">Option 04</td>
                                    <td class="Table_td">Option 05</td>
                                </tr>
                                <tr style="background-color:#B2DFDB">
                                    <td class="Table_desc">Limit</td>
                                    <td class="Table_td">1Mn</td>
                                    <td class="Table_td">2Mn</td>
                                    <td class="Table_td">3Mn</td>
                                    <td class="Table_td">4Mn</td>
                                    <td class="Table_td">5Mn</td>
                                </tr>
                                <tr style="background-color:#B2DFDB">
                                    <td class="Table_desc">Annual premium with taxes</td>
                                    <td class="Table_td">1,900/=</td>
                                    <td class="Table_td">2,500/=</td>
                                    <td class="Table_td">3,400/=</td>
                                    <td class="Table_td">4,300/=</td>
                                    <td class="Table_td">5,300/=</td>
                                </tr>
                            </table>
                            <br />
                            <p style="text-align: justify; text-justify: inter-word;">
                                (The above terms are subject to there were no losses/damages
                                 during last 10 years in respect of covered perils/ risks. If the
                                 insured suffered a loss in the past we ,Corporation General Ltd may
                                 organize a separate insurance in line with underwriting policy. )
                            </p>
                            <br />
                            <p><u><b>Conditions</b></u></p>
                            <ul class="b">
                                <li style="text-align: justify; text-justify: inter-word;">The Building should be used exclusively as a private dwelling house and no other purposes or domestic industries.</li>
                                <li style="text-align: justify; text-justify: inter-word; margin-top: 2px;">Owners should have acceptable security measures to safeguardhis/her own properties against any of the above perils.</li>
                                <li style="text-align: justify; text-justify: inter-word; margin-top: 2px;">The premises including properties should be in a good state of repair.</li>
                                <li style="text-align: justify; text-justify: inter-word; margin-top: 12px;">The construCtion should be bricks/concrete/cement block, walls and roofed with asbestos, concrete or tiles.</li>                          
                            </ul>
                            <br />
                            <p>
                                Deductible :10% or Rs.5,000/- each and every claim whichever is higher.
                            </p>
                            <br />
                            <p>
                                Insured property shall mean the building(s) and the contents therein as defined below
                            </p>
                            <br />
                             <ul>
                                <li style="text-align: justify; text-justify: inter-word;"><b>Building</b> shall mean main building, care taker room and the attached, swimming pool, parapet/perfect
                                    walls and gates, which includes permanent fixtures and fittings such as Air conditions, antennas, solar systems, CCTV systems and security systems etc. )</li>
                                <li style="margin-top:10px; text-align: justify; text-justify: inter-word;"><b>Contents</b> shall mean furniture, electrical and electronic appliances , cutlery & crockery excluding
                                    personal effects and other property specifically excluded in the "Section E" of the policy.</li>                               
                            </ul>
                        </div>
                    </div>
                 </div>
            <br />
             <div class="row">
				<div style=" margin: 0 auto; text-align: right;">
				<a href="HPL_2023_Purchase.aspx" class="btn btn-info" role="button">Buy Policy</a>
				</div>
			</div>
         </div>
       </div>   		
   <br />
   <br />

</asp:Content>
