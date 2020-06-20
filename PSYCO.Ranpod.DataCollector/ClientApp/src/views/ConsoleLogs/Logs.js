import React from 'react';
import { Paper } from '@material-ui/core';
import GridContainer from 'components/Grid/GridContainer';
import GridItem from 'components/Grid/GridItem';

import { makeStyles } from '@material-ui/styles';
import { PersonPinCircleOutlined, Add } from '@material-ui/icons';
import CustomTabs from 'components/CustomTabs/CustomTabs';
import AdminNavbarLinks from 'components/Navbars/AdminNavbarLinks';
import DetailedLog from './DetailedLog';
import ConsoleLogs from './ConsoleLogs';


const useStyles = makeStyles(theme => ({

    paper: {
        padding: theme.spacing(4),


    },
    grid: {
        [theme.breakpoints.up('md')]: {
            width: '50%',
        },

        [theme.breakpoints.down('sm')]: {
            width: '100%',
        },

        margin: '50px  auto'
    }


}))


export default function Logs(props) {

    const classes = useStyles();

    return (

        <React.Fragment>
            <GridContainer justify='center'>
                <GridItem xs={12} md={12}>
                    <Paper className={classes.paper}>

                        <CustomTabs
                            rtlActive
                            title=""
                            headerColor="primary"
                            tabs={[
                                {
                                    tabName: "GroupByed",
                                    // tabIcon: PersonPinCircleOutlined,
                                    tabContent: (
                                        <ConsoleLogs {...props} />

                                    )
                                },
                                {
                                    tabName: "Detailed",
                                    // tabIcon: Add,
                                    tabContent: (
                                        <DetailedLog {...props} />
                                    )
                                }

                            ]}
                        />
                    </Paper>
                </GridItem>

            </GridContainer>
        </React.Fragment>





        //     </GridItem>
        // </GridContainer>

    )

}