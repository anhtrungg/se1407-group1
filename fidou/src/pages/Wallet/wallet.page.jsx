import React, { useContext, useEffect, useState } from 'react'
import { getAuthen } from '../../axios/authenfunction'
import API from '../../constans/api'
import { TRANSACTION_TYPE } from '../../constans/enum'
import { AccountContext } from '../../context/AccountProvider'
import './wallet.style.scss'
const Wallet = () => {
  let [wallet, setWallet] = useState()
  let [transaction, setTransaction] = useState()
  const accountContext = useContext(AccountContext)
  const { data } = accountContext
  useEffect(() => {
    console.log(data.account)
    if (data.account) {
      getAuthen(API['GET_WALLET'] + '/' + data.account?.id)
        .then(response => {
          setWallet(response.data.data)
        })
        .catch()
      getAuthen(API['GET_TRANSACTION_HISTORY'], true)
        .then(response => {
          console.log(response.data.data)
          setTransaction(response.data.data)
        })
        .catch()
    }
  }, [data])

  return (
    <div className="wallet">
      <h3>
        Coin của bạn: <strong>{wallet?.availableBalance} β </strong>
      </h3>
      <h3>
        Coin đang kí quỹ: <strong>{wallet?.lockedBalance} β </strong>
      </h3>

      <h3>Nạp β coin vào ví:</h3>
      <div className="moneyWrapper">
        <div className="money">
          <div className="item">
            <strong>Chuyển tiền đến stk:</strong>
            <p>9967276988</p>
          </div>
          <div className="item">
            <strong>Ngân hàng:</strong>
            <p>MB Bank</p>
          </div>

          <div className="item">
            <strong>Nội dung:</strong>
            <p>VOICE {wallet?.depositCode}</p>
          </div>
        </div>

        <div className="momo">
          <strong className="momo__title">Thanh toán bằng ví MOMO</strong>
          <div className="item">
            <strong>Nội dung:</strong>
            <p>VOICE_MOMO {wallet?.depositCode}</p>
          </div>
          {/* <div className="momo__qr">
            <img src="/images/momo-qr-code.jpg" alt="momo-qr-code" />
          </div> */}
        </div>

        <div className="zalo">
          <strong className="zalo__title">Thanh toán bằng ví Zalo Pay</strong>
          <div className="item">
            <strong>Nội dung:</strong>
            <p>VOICE_ZALO {wallet?.depositCode}</p>
          </div>
          {/* <div className="zalo__qr">
            <img src="/images/zalopay.jpg" alt="zalo-qr-code" />
          </div> */}
        </div>
      </div>
      
      <div className="transaction">
        <h3 style={{ marginTop: '20px' }}>Lịch sử giao dịch:</h3>
        <table border={1}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Số tiền</th>
              <th>Thời gian</th>
              <th>Loại giao dịch</th>
            </tr>
          </thead>
          <tbody>
            {transaction?.map(item => {
              const date = new Date(item.createdTime)
              console.log(date)
              return (
                <tr>
                  <td>{item.adminId}</td>
                  <td>{item.amount}</td>
                  <td>{date.toDateString()}</td>
                  <td>{TRANSACTION_TYPE[item.transactionType]}</td>
                </tr>
              )
            })}
          </tbody>
        </table>
      </div>
    </div>
  )
}

export default Wallet
