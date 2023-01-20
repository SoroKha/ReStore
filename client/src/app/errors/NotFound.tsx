import { Button, Divider, Paper, Typography } from "@mui/material";
import { Container } from "@mui/system";
import { Link } from "react-router-dom";

export default function NotFound() {
    return (
        <Container component={Paper} sx={{height: 400}}>
            <Typography gutterBottom variant="h3">
                Oops! We couldn't find what you were looking for.
            </Typography>
            <Divider />
            <Button fullWidth component={Link} to='/catalogy'>
                Return to the shop
            </Button>
        </Container>
    )
}