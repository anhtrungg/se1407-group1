import React, { useContext, useEffect, useState } from 'react'
import { Link, useLocation, useNavigate, useParams } from 'react-router-dom'
import { getAuthen, postAuthen, putAuthen } from '../../axios/authenfunction'
import API from '../../constans/api'
import { ORDER_STATUS, TONE } from '../../constans/enum'
import LabelStatus from '../label-status/label-status.component'
import './jobdetail.style.scss'
import { addDoc, collection, db } from '../../Firebase/config'
import { AccountContext } from '../../context/AccountProvider'
import Swal from 'sweetalert2'
const JobDetail = () => {
  let { id, action } = useParams()
  const [job, setJob] = useState()
  const [category, setCategory] = useState()
  const [enterprise, setEnterprise] = useState()
  const location = useLocation()
  const [isAccept, setIsAccept] = useState(false)
  const navigate = useNavigate()

  useEffect(() => {
    getAuthen(API['GET_JOB'] + `${id}`)
      .then(response => {
        console.log('response.data.data: ', response.data.data)
        setJob(response.data.data)
      })
      .catch()
  }, [location])

  useEffect(() => {
    if (location.search.split('=')[1] == 'accept') {
      setIsAccept(true)
    }
  }, [location])

  useEffect(() => {
    if (job) {
      getAuthen(API['GET_SUBCATEGORY'] + `/${job.subCategoryId}`)
        .then(response => {
          setCategory(response.data.data)
        })
        .catch()

      getAuthen(API['GET_ENTERPRISE_INFO'] + `${job.enterpriseId}`)
        .then(response => {
          setEnterprise(response.data.data)
        })
        .catch()
    }
  }, [job])

  const accountContext = useContext(AccountContext)
  let { data } = accountContext

  const acceptJob = status => {
    console.log(enterprise.id)
    //   enterprise.id,
    //   data.account?.id,
    //   `Group chat ${data.jobInvitation.job.name} (Invite Job)`,
    //   data.transaction.order.id);
    putAuthen(
      API['ACCEPT_JOB'],
      {
        id: id,
        status: status
      },
      true
    ).then(response => {
      if (status == 2) {
        const dataResponse = response.data.data
        createRoom(
          enterprise.id,
          data.account?.id,
          `Ph??ng trao ?????i ${dataResponse.jobInvitation.job.name} (M???i v??o l??m vi???c)`,
          dataResponse.transaction.order.id
        )
        setJob({
          ...job,
          jobStatus: 1
        })
        Swal.fire(
          'Th??ng b??o!',
          'Tham gia v??o d??? ??n th??nh c??ng!',
          'success'
        ).then(response => {
          navigate(location.pathname)
        })
      }

      if (status == 1) {
        Swal.fire(
          'Th??ng b??o!',
          'T??? ch??i v??o d??? ??n th??nh c??ng!',
          'success'
        ).then(response => {
          navigate(location.pathname)
        })
      }
    })
  }

  const createRoom = (id1, id2, title, orderId) => {
    const date = new Date()
    const dateString =
      date.getDate() +
      '/' +
      date.getMonth() +
      '/' +
      date.getFullYear() +
      ' ' +
      date.getHours() +
      ':' +
      date.getMinutes()
    addDoc(collection(db, 'room'), {
      title: title,
      description: 'Ph??ng trao ?????i',
      lastSent: dateString,
      user: [id1, id2],
      orderId: orderId
    })
      .then(response => {
        console.log(response)
      })
      .catch(error => {
        console.log(error)
      })
  }

  const handleApplyJob = () => {
    const fee = job.price * 0.1
    Swal.fire({
      title: 'B???n c?? mu???n ti???p t???c?',
      text:
        'Khi ???ng tuy???n v??o d??? n??y b???n s??? k?? qu??? ' + fee + '?? trong t??i kho???n.',
      icon: 'info',
      showCancelButton: true,
      confirmButtonColor: '#79be82',
      cancelButtonColor: '#d33',
      confirmButtonText: '???ng tuy???n'
    }).then(result => {
      if (result.isConfirmed) {
        postAuthen(
          API['POST_ORDER'],
          {
            jobId: job.id
          },
          true
        )
          .then(response => {
            Swal.fire(
              'Th??ng b??o!',
              'N???p ????n v??o d??? ??n ' + job.name + ' th??nh c??ng',
              'success'
            )
          })
          .catch(error => {
            Swal.fire({
              icon: 'error',
              title: 'Oops...',
              text: error.response.data.msg
            })
          })
      }
    })
  }

  return (
    <div className="jobdetail">
      <h1>{job?.name}</h1>
      <div className="jobdetail__company__info">
        <strong>
          <Link to={`/company/${enterprise?.id}`}>
            <div className="enterprise__info">
              <img src={enterprise?.logoUrl} alt="" />
              {enterprise?.name}
            </div>
          </Link>
          {' - '}
          {new Date(job?.createdTime).toLocaleDateString()}
        </strong>
      </div>

      <div className="jobdetail__content box">
        <div className="item">
          <p>
            <strong>M?? t???: </strong>
          </p>

          <p>{job?.description}</p>
        </div>
        <div className="item">
          <p>
            <strong>????? tu???i: </strong>
          </p>
          <p>
            {job?.minAge} - {job?.maxAge}
          </p>
        </div>

        <div className="item">
          <p>
            <strong>Gi???i t??nh: </strong>
          </p>
          <p>
            {(() => {
              switch (job?.gender) {
                case 0:
                  return 'Nam'
                case 1:
                  return 'N???'
                default:
                  return 'Kh??ng y??u c???u'
              }
            })()}
          </p>
        </div>

        <div className="item">
          <p>
            <strong>Ng??n ng???: </strong>
          </p>
          <p>
            {(() => {
              switch (job?.language) {
                case 0:
                  return 'Ti???ng Vi???t'
                case 1:
                  return 'Ti???ng Anh'
                default:
                  return 'Kh??c'
              }
            })()}
          </p>
        </div>

        <div className="item">
          <strong>Th???i gian: </strong>

          {job?.dayDuration ? (
            <LabelStatus
              state={'info'}
              label={`${job?.dayDuration} ng??y`}
              size={'large'}
            />
          ) : (
            <></>
          )}

          {job?.hourDuration ? (
            <LabelStatus
              state={'info'}
              label={`${job?.hourDuration} gi???`}
              size={'large'}
            />
          ) : (
            <></>
          )}

          {job?.minuteDuration ? (
            <LabelStatus
              state={'info'}
              label={`${job?.minuteDuration} ph??t`}
              size={'large'}
            />
          ) : (
            <></>
          )}
        </div>

        <div className="item">
          <strong>Th??? lo???i: </strong>
          {category ? (
            <LabelStatus state={'info'} label={category.name} size={'large'} />
          ) : (
            <></>
          )}
        </div>

        <div className="item">
          <strong>Tr???ng th??i: </strong>
          <LabelStatus
            label={ORDER_STATUS[job?.jobStatus]?.title}
            state={ORDER_STATUS[job?.jobStatus]?.state}
            size={'large'}
          />
        </div>

        <div className="item">
          <strong>Ch???t gi???ng: </strong>
          <LabelStatus label={TONE[job?.tone]} state={'info'} size={'large'} />
        </div>

        <div className="item">
          <strong>Gi??: </strong>
          <span>{job?.price}??</span>
        </div>
        {isAccept && job?.jobStatus == 0 ? (
          <div className="accept_button">
            <button
              className="button"
              onClick={() => {
                acceptJob(2)
              }}
            >
              Ch???p nh???n c??ng vi???c
            </button>

            <button
              className="button inject"
              onClick={() => {
                acceptJob(1)
              }}
            >
              T??? ch???i c??ng vi???c
            </button>
          </div>
        ) : (
          <>
            {data.account?.role == 0 && job?.jobStatus == 0 ? (
              <div className="accept_button">
                <button className="button" onClick={handleApplyJob}>
                  ???ng tuy???n
                </button>
              </div>
            ) : (
              <></>
            )}
          </>
        )}
      </div>
    </div>
  )
}

export default JobDetail
