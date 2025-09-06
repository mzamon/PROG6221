using System.Reflection.Metadata;
using System.Security.Claims;
using System.Threading;

using System;

< div class= "coordinator-view" >
    < h2 > Coordinator Claim Verification</h2>
    <p>Below are claims submitted by lecturers (sample data)</p>
    <table class= "claims-table" >
        < thead >
            < tr >
                < th > Lecturer </ th >
                < th > Hours </ th >
                < th > Rate </ th >
                < th > Notes </ th >
                < th > Documents </ th >
                < th > Actions </ th >
            </ tr >
        </ thead >
        < tbody >
            < tr >
                < td > Mzamo Ndlovu </ td >
                < td > 10 </ td >
                < td > R550 </ td >
                < td > Tutor classes </ td >
                < td >< a href = "#" > view.pdf </ a ></ td >
                < td >
                    < button class= "btn-approve" > Approve </ button >
                    < button class= "btn-reject" > Reject </ button >
                </ td >
            </ tr >
        </ tbody >
    </ table >
</ div >
