import { Add, Delete, Remove } from "@mui/icons-material";
import { LoadingButton } from "@mui/lab";
import { Box, Button, Grid, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from "@mui/material";
import { Link } from "react-router-dom";
import { useAppDispatch, useAppSelector } from "../../app/store/configureStore";
import { addBasketItemAsync, removeBasketItemAsync } from "./basketSlice";
import BasketSummary from "./BasketSummary";

export default function BasketPage() {
    const { basket, status } = useAppSelector(state => state.basket);
    const dispatch = useAppDispatch();


   

    if (!basket) return <Typography variant="h3">Your basket is empty!</Typography>

    return (
    <>
        <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }}>
          <TableHead>
            <TableRow>
              <TableCell>Product</TableCell>
              <TableCell align="right">Price</TableCell>
              <TableCell align="center">Quantity</TableCell>
              <TableCell align="right">Subtotal</TableCell>
              <TableCell align="right"></TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {basket.items.map((item) => (
              <TableRow
                key={item.productID}
                sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  <Box display='flex' alignItems='center'>
                    <img src={item.pictureURL} alt={item.name} style={{height: 50, marginRight: 20}} />
                    <span>{item.name}</span>
                  </Box>
                </TableCell>
                <TableCell align="right">${(item.price / 100).toFixed(2)}</TableCell>
                <TableCell align="center">
                    <LoadingButton
                    loading={status === 'pendingRemoveItem' + item.productID + 'rem'}
                    onClick={() => dispatch(removeBasketItemAsync({
                      productID: item.productID, quantity: 1, name: 'rem'
                      }))}
                      color="error">
                        <Remove />
                    </LoadingButton>
                    {item.quantity}
                    <LoadingButton
                    loading={status === 'pendingAddItem' + item.productID}
                    onClick={() => dispatch(addBasketItemAsync({productID: item.productID}))} color="secondary">
                        <Add />
                    </LoadingButton>
                </TableCell>
                <TableCell align="right">${((item.price / 100) * item.quantity).toFixed(2)}</TableCell>
                <TableCell align="right">
                    <LoadingButton
                        loading={status === 'pendingRemoveItem' + item.productID + 'del'}
                        onClick={() => dispatch(removeBasketItemAsync({
                          productID: item.productID, quantity: item.quantity, name: 'del'
                        }))}
                        color='error'
                    >
                        <Delete />
                    </LoadingButton>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
          
        </Table>
      </TableContainer> 
      <Grid container>
        <Grid item xs={6} />
        <Grid item xs={6} />
        <BasketSummary />
        <Button
          component={Link}
          to='/checkout'
          variant="contained"
          size="large"
          fullWidth
        >
          Checkout
        </Button>
      </Grid>
      </>
    )
}