import React, { useEffect, useState } from 'react';
import Card from 'components/Card/Card';
import { CardContent, Typography, Link, CircularProgress } from '@material-ui/core';
import { CheckCircleOutline } from '@material-ui/icons';
import queryString from 'query-string'
import { MainService } from 'services/MainService';
import { makeStyles } from '@material-ui/styles';
import { Link as RouterLink } from 'react-router-dom';
import { ShowSnack } from 'infrastructure/Helper/Showsnack';

const useStyle = makeStyles(theme=>({
    card : {
        maxWidth : '300px',
        margin : '100px auto'

    }
}))
const Link1 = React.forwardRef((props, ref) => <RouterLink innerRef={ref} {...props} />);

export default function ConfirmEmail(props) {
    const classes = useStyle();
    const querystrings = queryString.parse(props.location.search);
    const [done, setDone] = useState(false);
    useEffect(() => {
        
        
        MainService.Get(`/api/account/confirm?code=${encodeURIComponent(querystrings.code)}&uid=${querystrings.uid}`).then(() =>
            setDone(true)

        ,error=> {

            ShowSnack.Error(error);
        } )
    })

    return (
        done ?
        <Card className={classes.card}>
            <CardContent >
              
                <Typography  align='center' variant='h2'  >
                    <CheckCircleOutline fontSize='large'   color='secondary' />
                </Typography>
                <Typography align='center' variant='h6' color='textSecondary'>
                    ایمیل شما تایید شد.اکنون می توانید <Link component={Link1} to="/login-register"  >وارد</Link> شوید.
                </Typography>
            </CardContent>
        </Card>
        : <div style={{
            display: 'flex',
            justifyContent: 'center'
        }}>

            <CircularProgress
                // className={classes.progress}
                color="secondary" />
            
        </div>
    )


}

